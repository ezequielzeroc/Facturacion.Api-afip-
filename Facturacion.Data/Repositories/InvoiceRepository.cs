using Facturacion.AFIP;
using Facturacion.Data.Contracts;
using Facturacion.Data.Handlers;
using Facturacion.Data.Models.Invoices;
using Facturacion.Domain;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Facturacion.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private EasyStcokDBContext _dbContex;
        public InvoiceRepository(EasyStcokDBContext dbContext)
        {
            _dbContex = dbContext;
        }


        public async Task<InvoiceResponseModel> Create(int CompanyID, Invoices invoice)
        {
            BIWSFEV1 wsfev1;
            List<InvoiceAuthorizationObs> _observations;
            int nroComprobante;
            InvoiceResponseModel response = new InvoiceResponseModel();
            List<Taxes> taxes = null;
            Certificates cert = null;
            DocumentType documentType = new DocumentType();
            List<(string, decimal)> AmountTaxes = new List<(string, decimal)>();
            List<TaxesModel> lstTaxes = new List<TaxesModel>();
            decimal CalculatedTaxes = 0;
            decimal BaseTaxable = 0;
            decimal CalculatedDiscount = 0;
            double NetAmount=0;
            try
            {
                _observations = new List<InvoiceAuthorizationObs>();
                cert = await _dbContex.Certificates.FirstOrDefaultAsync(x => x.CompanyID == CompanyID);
                wsfev1 = new BIWSFEV1();
                taxes = await _dbContex.Taxes.ToListAsync();
                documentType = await _dbContex.DocumentTypes.FirstOrDefaultAsync(x => x.DocumentTypeID == invoice.DocumentTypeID);


                response = new InvoiceResponseModel();
                invoice.CompanyID = CompanyID;

                invoice.Letter = documentType.Letter;
                invoice.DocumentTypeCode = documentType.Code.ToString().PadLeft(3,'0');
                invoice.DocumentTypeShortCode = documentType.ShortName;

                await _dbContex.AddAsync(invoice);
                invoice.Status = Enums.InvoiceStatus.Created;


                #region Extraer impuestos y calcular descuentos
                var GroupedTaxes = invoice.Items.GroupBy(x => x.TaxId);
                foreach (var item in GroupedTaxes)
                {
                    int TaxCode = int.Parse(item.Key.ToString());
                    Taxes Tax = taxes.FirstOrDefault(x => x.Code == TaxCode);
                    //var TotalDiscount = item.Sum(x => x.dis);
                    CalculatedTaxes = item.Sum(x => x.TaxCalculated);
                    BaseTaxable = item.Sum(x => x.Price * x.Qtty);
                    lstTaxes.Add(new TaxesModel { CalculatedTax = (double)CalculatedTaxes, TaxBase = (double)BaseTaxable, TaxID = TaxCode });
                    AmountTaxes.Add((Tax.Name, CalculatedTaxes));
                    CalculatedTaxes = 0;

                }
                NetAmount = double.Parse(lstTaxes.Sum(x => x.TaxBase).ToString("N2")); //neto gravado
                if (cert != null)
                {

                    if (await wsfev1.login(cert.Path, cert.Password))
                    {
                        var LastInvoice = await wsfev1.recuperaLastCMPAsync(invoice.PosCode, documentType.Code);
                        nroComprobante = LastInvoice.Body.FECompUltimoAutorizadoResult.CbteNro;

                        nroComprobante++;
                        wsfev1.reset();
                        wsfev1.agregaFactura(int.Parse(invoice.ConceptCode), documentType.Code, int.Parse(invoice.IdentityDocumentNumber), nroComprobante, nroComprobante, DateTime.Parse(DateTime.Now.ToShortDateString()), Helpers.Truncate((double)invoice.Total,2), /*TotalNotTaxed*/0, Helpers.Truncate(NetAmount,2), /*OptionalExemptAmount*/ 0, null, null, null, "PES", 1);

                        if (lstTaxes != null)
                        {
                            if (lstTaxes.Count > 0)
                            {
                                foreach (var tax in lstTaxes)
                                {
                                    wsfev1.agregaIVA(tax.TaxID, Handlers.Helpers.Truncate(tax.TaxBase,2), Handlers.Helpers.Truncate(tax.CalculatedTax,2));
                                }
                            }
                        }

                        var Authorizar = await wsfev1.AutorizarAsync(invoice.PosCode, documentType.Code);
                        //wsfev1.autorizarRespuesta(0, ref cae, ref vencimiento, ref resultado);
                        var respuesta = wsfev1.autorizarRespuestaV2(0);
                        if (respuesta.Resultado == "A")
                        {
                            Guid id = Guid.NewGuid();

                            invoice.CAE = respuesta.CAE;
                            invoice.CAEExpiration = respuesta.VencimientoCae;
                            response.Result = respuesta.Resultado;

                        /*GENERACION DE CODIGO DE BARRA*/
                            string FileCodeBar = $"{id}" +
                                $".bmp";
                            string PathCodeBar = System.IO.Path.Combine($"C:\\Invoices\\Barcode\\{FileCodeBar}");
                            FEAFIPLib.TBarcodeBitmap.generarCodigoBarras(long.Parse(invoice.IdentityDocumentNumber), (byte)int.Parse(invoice.DocumentTypeCode), (byte)invoice.PosCode, respuesta.CAE, respuesta.VencimientoCae, 3, 80, PathCodeBar);

                            BarCode barCode = new BarCode
                            {
                                Created = DateTime.Today,
                                Name = FileCodeBar
                            };

                            invoice.Status = Enums.InvoiceStatus.Authorized;

                            invoice.BarCode = barCode;
                            invoice.InvoiceNumber = nroComprobante;
                        }
                        else
                        {
                            if (respuesta.Observaciones.Count > 0)
                            {
                                foreach (var obs in respuesta.Observaciones)
                                {
                                    _observations.Add(new InvoiceAuthorizationObs
                                    {
                                        Code = obs.Codigo,
                                        Description = obs.Descripcion
                                    });
                                }
                                response.Observations = _observations;
                            }
                        }

                    }
                }

                #endregion
                string fileName = await CreatePDF(invoice,AmountTaxes);

                Download download = new Download
                {
                    File = fileName,
                    Created = DateTime.Now,
                    Count = 0,
                };

                invoice.Download = download;

                await _dbContex.SaveChangesAsync();
                response.InvoiceID = invoice.InvoiceID;
                response.Created = DateTime.Now;
                response.CAE = invoice.CAE;
                response.DueDateCae = invoice.CAEExpiration?.ToShortDateString();
                return response;



                //let calculatedDiscount = 0;
                //let discountPercent = discount / 100;
                //let _subTotal = 0;
                //let _total = this.ToDecimal(0);

                //calculatedDiscount = (price * Qtty) * discountPercent;

                //_subTotal = this.ToDecimal((price * Qtty) - calculatedDiscount);


                //let tax = Tax.taxById(parseInt(taxId));
                //let taxValue = tax.Value;
                //let TaxCalculated = (_subTotal * taxValue) / 100;
                //_total = this.ToDecimal(parseFloat(_subTotal) + parseFloat(TaxCalculated));



            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Invoices>> GetInvoices(int CompanyID)
        {
            IEnumerable<Invoices> result;
            IQueryable<Invoices> QueryInvoice;
            try
            {
                QueryInvoice = _dbContex.Invoices.AsQueryable();
                QueryInvoice.Where(x => x.CompanyID == CompanyID);
                result = await QueryInvoice.ToListAsync();
                
            }
            catch (Exception e)
            {

                throw;
            }

            return result;
        }


        public async Task<string> CreatePDF(Invoices invoice, List<(string,decimal)> taxes)
        {
            int CompanyID = invoice.CompanyID;
            CompanySettings settings = new CompanySettings();

            settings = await _dbContex.CompanySettings.FirstOrDefaultAsync(x => x.CompanyID == CompanyID);

            //Clients client = await _DbContext.Clients.FirstOrDefaultAsync(x => x.ClientID == invoice.ClientID);

            string FileName = $"{invoice.DocumentTypeCode}{invoice.InvoiceID}{Guid.NewGuid()}.pdf";
            PdfWriter pw = new PdfWriter($"C:\\Invoices\\{FileName}");
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(10, 10, 10, 10);
            Table Header = new Table(UnitValue.CreatePercentArray(new float[] { 40, 10, 40 })).UseAllAvailableWidth().SetBorder(Border.NO_BORDER);
            Table Client = new Table(1).UseAllAvailableWidth();
            Table Conditions = new Table(2).UseAllAvailableWidth();
            Table HeaderItems = new Table(UnitValue.CreatePercentArray(new float[] { 10, 40, 10, 10, 10, 10, 10 })).UseAllAvailableWidth();
            Table DetailsItems = new Table(UnitValue.CreatePercentArray(new float[] { 10, 40, 10, 10, 10, 10, 10 })).UseAllAvailableWidth();
            Table HeaderCompany = new Table(1).UseAllAvailableWidth();
            HeaderCompany.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph(settings.BusinessName)
                .SetFontSize(24)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderCompany.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph(settings.ShortName)
                .SetFontSize(14)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderCompany.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Razón social: {settings.BusinessName}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT)));
            HeaderCompany.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Domicilio comercial: {settings.FiscalAddress}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT)));

            HeaderCompany.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Condición frente al IVA: Responsable inscripto ")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT))).SetBorder(Border.NO_BORDER);
            Table HeaderLetter = new Table(UnitValue.CreatePercentArray(new float[] { 10 }))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            HeaderLetter.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                //.Add(new Paragraph(invoice.Letter)
                .Add(new Paragraph(invoice.Letter)
                .SetFontSize(20)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderLetter.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"cod. {invoice.DocumentTypeCode}")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(9)));


            Table HeaderInformation = new Table(1).UseAllAvailableWidth();
            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("FACTURA")
                .SetFontSize(16)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Nro: {invoice?.PosCode.ToString().PadLeft(3, '0')}-{invoice?.InvoiceNumber.ToString().PadLeft(8, '0')}")
                .SetFontSize(14)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Fecha: {invoice.Created?.ToString("dd/MM/yyyy")}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)));
            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"C.U.I.T.: NRO: {settings.Cuit}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT)));

            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Ing. Brutos: {settings.GrossIncomeNumber}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT))).SetBorder(Border.NO_BORDER);
            HeaderInformation.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Inicio actividades: {settings.StartActivities}")
                .SetFontSize(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT)));
            Header.AddCell(HeaderCompany);
            Header.AddCell(HeaderLetter);
            Header.AddCell(HeaderInformation);




            #region Client

            Table ClientSection = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 })).UseAllAvailableWidth();

            ClientSection.AddCell(new Cell()
               .SetBorder(Border.NO_BORDER)
               .Add(new Paragraph($"Razón social: {invoice.ClientName}")
               .SetFontSize(10)
               .SetBold()));
            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Cuenta Nro: 00000")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Domicilio: {invoice.ClientAddress}")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"Teléfono: {""}")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Localidad: -")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Cod. Postal:  -")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph("Provincia: -")
                .SetFontSize(10)
                .SetBold()));

            ClientSection.AddCell(new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(new Paragraph($"CUIT/DNI: {invoice.IdentityDocumentNumber}")
                .SetFontSize(10)
                .SetBold()));


            Client.AddCell(ClientSection);
            #endregion
            Conditions.AddCell(new Cell()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .Add(new Paragraph("Condición de venta: Venta contado")
                .SetFontSize(10)));
            Conditions.AddCell(new Cell().Add(new Paragraph($"Vencimiento de pago: {invoice.Created?.AddDays(10).ToString("dd/MM/yyyy")}").SetFontSize(10)));

            #region Items
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("Código")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("Descripción")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("Cantidad")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.RIGHT)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("U. Medida")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("P. Unitario")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.RIGHT)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("Bonif.")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.RIGHT)));
            HeaderItems.AddCell(new Cell()
                .Add(new Paragraph("Total")
                .SetFontSize(9)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.RIGHT)));


            foreach (var item in invoice.Items)
            {
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph(item.ItemID.ToString().PadLeft(6, '0'))
                    .SetFontSize(9)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph(item?.Description)
                    .SetFontSize(9)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph(item?.Qtty.ToString())
                    .SetFontSize(9)
                    .SetTextAlignment(TextAlignment.RIGHT)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph("Unidades")
                    .SetFontSize(9)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph($"$ {item.Price.ToString("N")}")
                    .SetFontSize(9)
                    .SetTextAlignment(TextAlignment.RIGHT)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph($"$ {item.TotalDiscount.ToString("N")}")
                    .SetFontSize(9)
                    .SetTextAlignment(TextAlignment.RIGHT)));
                DetailsItems.AddCell(new Cell()
                    .Add(new Paragraph($"$ {(item.SubTotal).ToString("N")}")
                    .SetFontSize(9)
                    .SetTextAlignment(TextAlignment.RIGHT)));
            }




            #endregion

            #region Totales
            Table Footer = new Table(1).UseAllAvailableWidth();
            Table Totals = new Table(UnitValue.CreatePercentArray(new float[] { 20, 20 }));
            Table BarCode = new Table(1).UseAllAvailableWidth().SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Totals.AddCell(new Cell()
                .Add(new Paragraph("Subtotal")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.AddCell(new Cell()
                .Add(new Paragraph($"$ {invoice.Subtotal.ToString("N2")}")
                .SetFontSize(11)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            Totals.AddCell(new Cell()
                .Add(new Paragraph("Importe Bonif.")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.AddCell(new Cell()
                .Add(new Paragraph($"$ {invoice.TotalDiscount.ToString("N2")}")
                .SetFontSize(11)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            Totals.AddCell(new Cell()
                .Add(new Paragraph("Subtotal c/ Bonif")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.AddCell(new Cell()
                .Add(new Paragraph("$ 0.00")
                .SetFontSize(11)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            Totals.AddCell(new Cell()
                .Add(new Paragraph("Importe otros tributos")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.AddCell(new Cell()
                .Add(new Paragraph("$ 0.00")
                .SetFontSize(11)
                .SetTextAlignment(TextAlignment.RIGHT)));

            foreach (var tax in taxes)
            {
                Totals.AddCell(new Cell()
                .Add(new Paragraph(tax.Item1) //nombre
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT)));
                Totals.AddCell(new Cell()
                    .Add(new Paragraph($"$ {tax.Item2.ToString("N")}")
                    .SetFontSize(11)
                    .SetTextAlignment(TextAlignment.RIGHT)));

            }
            Totals.AddCell(new Cell()
                .Add(new Paragraph("Importe Total")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.AddCell(new Cell()
                .Add(new Paragraph($"$ {(invoice.Total).ToString("N")}")
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.RIGHT)));
            Totals.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            Totals.SetHorizontalAlignment(HorizontalAlignment.RIGHT);


            ImageData imageData = ImageDataFactory.Create($"C:\\Invoices\\Barcode\\default.bmp");

            if (invoice.BarCode!=null)
            {
                imageData = ImageDataFactory.Create($"C:\\Invoices\\Barcode\\{invoice.BarCode?.Name}");
            }
            

            // Create layout image object and provide parameters. Page number = 1
            Image image = new Image(imageData).ScaleAbsolute(200, 40);
            BarCode.AddCell(image).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Table BarCodeInfo = new Table(2).UseAllAvailableWidth();
            Table DateCae = new Table(1).UseAllAvailableWidth();

            DateCae.AddCell(new Cell().Add(new Paragraph($"CAE: {invoice.CAE}")).SetBorder(Border.NO_BORDER));
            DateCae.AddCell(new Cell().Add(new Paragraph($"Fecha vencimiento CAE: {invoice.CAEExpiration?.ToString("dd/MM/yyyy")}")).SetBorder(Border.NO_BORDER));
            BarCodeInfo.AddCell(BarCode).SetBorder(Border.NO_BORDER);
            BarCodeInfo.AddCell(DateCae).SetBorder(Border.NO_BORDER);

            Footer.AddCell(Totals).UseAllAvailableWidth(); ;
            Footer.AddCell(BarCodeInfo).UseAllAvailableWidth(); ;

            PageSize ps = pdfDocument.GetDefaultPageSize();
            Footer.SetFixedPosition(doc.GetLeftMargin(), doc.GetBottomMargin(), ps.GetWidth() - doc.GetLeftMargin() - doc.GetRightMargin());
            #endregion


            doc.Add(Header);
            doc.Add(Client);
            doc.Add(Conditions);
            doc.Add(HeaderItems);
            doc.Add(DetailsItems);
            doc.Add(Footer);

            doc.Close();
            return FileName;
        }

        public async Task<FileStream> Download(int InvoiceID)
        {
            string path = "";
            try
            {
                Download download = await _dbContex.Downloads.FirstOrDefaultAsync(x => x.InvoiceFK == InvoiceID);

                if(download!=null)
                {
                    path = download.File;
                }
                var fs = System.IO.File.OpenRead("C:\\Invoices\\" + path);
                return fs;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int InvoiceID)
        {
            Invoices invoice = null;
            try
            {
                invoice = await _dbContex.Invoices.FirstOrDefaultAsync(x => x.InvoiceID == InvoiceID);
                _dbContex.Entry(invoice).State = EntityState.Deleted;
                await _dbContex.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
