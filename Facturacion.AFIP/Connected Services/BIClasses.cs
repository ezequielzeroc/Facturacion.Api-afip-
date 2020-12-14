
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using Facturacion.AFIP.wsfev1;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Facturacion.Domain;
using System.IO;

namespace Facturacion.AFIP
{

    public class BIWSFEV1
    {
        public FECompConsultaResponse _Comp { get; set; }
        private const string dateFormat = "yyyyMMdd";
        public int mErrorCode;
        public string mErrorDesc;
        private wsfev1.FEAuthRequest mAuthRequest = new wsfev1.FEAuthRequest();
        private wsfev1.FECAERequest mFECAERequest;
        private wsfev1.FECAESolicitarResponse fecaeSolicitarResponse;
        private const string URLWSAA = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string URLWSAA_HOMO = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string URLWSW = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
        private const string URLWSW_HOMO = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx";
        private const string URLWSServiceA4 = "https://aws.afip.gov.ar/sr-padron/webservices/personaServiceA4";
        private const string URLWSServiceA4_HOMO = "https://awshomo.afip.gov.ar/sr-padron/webservices/personaServiceA4";
        private const string URLWSServiceA5 = "https://aws.afip.gov.ar/sr-padron/webservices/personaServiceA5";
        private const string URLWSServiceA5_HOMO = "https://awshomo.afip.gov.ar/sr-padron/webservices/personaServiceA5";
        private wsfev1.ServiceSoapClient mClient;
        private bool mModoProduccion;
        private string mCertificadoPFX;
        private string mPassword;

        private FECAEAResponse mCAEAInformarResponse;

        public BIWSFEV1()
        {
            //Dim newCulture As CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture.Clone
            //newCulture.DateTimeFormat.ShortDatePattern = "yyyyMMdd"
            //newCulture.DateTimeFormat.DateSeparator = ""
            //Thread.CurrentThread.CurrentCulture = newCulture
        }

        private wsfev1.ServiceSoapClient getClient()
        {

            if (mClient == null)
            {
                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.Transport;

                if (mModoProduccion)
                {
                    mClient = new wsfev1.ServiceSoapClient(binding, new EndpointAddress(URLWSW));
                }
                else
                {
                    mClient = new wsfev1.ServiceSoapClient(binding, new EndpointAddress(URLWSW_HOMO));
                }


            }
            return mClient;
        }

        public async Task<bool> login(string certificadoPFX, string password, string servicio = "wsfe")
        {
            mCertificadoPFX = certificadoPFX;
            mPassword = password;
            LoginTicket loginTicket = new LoginTicket();
            try
            {
                var url = mModoProduccion ? URLWSAA : URLWSAA_HOMO;
                 await loginTicket.ObtenerLoginTicketResponse(servicio, url, certificadoPFX, password);
                mAuthRequest = new wsfev1.FEAuthRequest();
                mAuthRequest.Token = loginTicket.Token;
                mAuthRequest.Sign = loginTicket.Sign;
                mAuthRequest.Cuit = (long)loginTicket.CUIT;

                return true;
            }
            catch (Exception e)
            {
                mErrorCode = -1;
                mErrorDesc = e.Message;
                return false;
            }
        }

        public int ErrorCode
        {
            get { return mErrorCode; }
        }

        public string ErrorDesc
        {
            get { return mErrorDesc; }
        }

        public bool ModoProduccion
        {
            get { return mModoProduccion; }
            set
            {
                mModoProduccion = value;
                //mModoProduccion = false;
                //Interaction.MsgBox("La siguiente dll esta habilitada en modo Demo. Para obtener la licencia en produccion contacte a contacto@bitingenieria.com.ar");
            }
        }

        public async Task<FECompUltimoAutorizadoResponse> recuperaLastCMPAsync(int ptoVta, int tipoComprobante)
        {
            wsfev1.FECompUltimoAutorizadoResponse result = await getClient().FECompUltimoAutorizadoAsync(mAuthRequest, ptoVta, tipoComprobante);
            return result;
        }

        public void reset()
        {
            mFECAERequest = new wsfev1.FECAERequest();
            mFECAERequest.FeCabReq = new wsfev1.FECAECabRequest();
            List<wsfev1.FECAEDetRequest> detail = new List<wsfev1.FECAEDetRequest>();
            mFECAERequest.FeDetReq = detail.ToArray();
        }

        public void agregaFactura(int concepto, int docTipo, long docNro, long cbteDesde, long cbteHasta, System.DateTime cbteFch, double impTotal, double impTotalConc, double impNeto, double impOpEx,
        System.DateTime? FchServDesde, System.DateTime? FchServHasta, System.DateTime? FchVtoPago, string monId, double monCotiz)
        {
            if (mFECAERequest == null)
            {
                reset();
            }
            FECAEDetRequest[] detRequests = mFECAERequest.FeDetReq;
            Array.Resize(ref detRequests, mFECAERequest.FeDetReq.Length + 1);
            mFECAERequest.FeDetReq = detRequests;
            mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.Length - 1] = new FECAEDetRequest();
            var _with1 = mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.Length - 1];
            _with1.CbteDesde = cbteDesde;
            _with1.CbteFch = cbteFch.Date.ToString(dateFormat);
            _with1.CbteHasta = cbteHasta;
            _with1.Concepto = concepto;
            _with1.DocNro = docNro;
            _with1.DocTipo = docTipo;
            if ((FchServDesde != null))
            {
                _with1.FchServDesde = FchServDesde.Value.Date.ToString(dateFormat);
            }
            if ((FchServHasta != null))
            {
                _with1.FchServHasta = FchServHasta.Value.Date.ToString(dateFormat);
            }
            if ((FchVtoPago != null))
            {
                _with1.FchVtoPago = FchVtoPago.Value.Date.ToString(dateFormat);
            }
            _with1.ImpNeto = impNeto;
            _with1.ImpOpEx = impOpEx;
            _with1.ImpTotal = impTotal;
            _with1.ImpTotConc = impTotalConc;
            _with1.MonCotiz = monCotiz;
            _with1.MonId = monId;
            mFECAERequest.FeCabReq.CantReg = mFECAERequest.FeDetReq.Length;
        }

        public void agregaIVA(int id, double baseImp, double importe)
        {
            if (mFECAERequest.FeDetReq.Length > 0)
            {
                FEDetRequest lDetRequest = mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.GetUpperBound(0)];
                AlicIva[] alicIvas = lDetRequest.Iva;
                if (lDetRequest.Iva == null)
                {
                    Array.Resize(ref alicIvas, 1);
                }
                else
                {
                    Array.Resize(ref alicIvas, alicIvas.Length + 1);
                }
                lDetRequest.Iva = alicIvas;
                lDetRequest.Iva[lDetRequest.Iva.GetUpperBound(0)] = new AlicIva();
                var _with2 = lDetRequest.Iva[lDetRequest.Iva.GetUpperBound(0)];
                _with2.BaseImp = baseImp;
                _with2.Id = id;
                _with2.Importe = importe;
                decimal decImp = (decimal)(lDetRequest.ImpIVA + importe);
                lDetRequest.ImpIVA = (double)decImp;
            }
        }

        public void agregaTributo(int id, string desc, double baseImp, double alic, double importe)
        {
            if (mFECAERequest.FeDetReq.Length > 0)
            {
                FECAEDetRequest lDetRequest = mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.GetUpperBound(0)];
                Tributo[] tributos = lDetRequest.Tributos;
                if (lDetRequest.Tributos == null)
                {
                    Array.Resize(ref tributos, 1);
                }
                else
                {
                    Array.Resize(ref tributos, tributos.Length + 1);
                }
                lDetRequest.Tributos = tributos;
                lDetRequest.Tributos[lDetRequest.Tributos.GetUpperBound(0)] = new Tributo();
                var _with3 = lDetRequest.Tributos[lDetRequest.Tributos.GetUpperBound(0)];
                _with3.Alic = alic;
                _with3.BaseImp = baseImp;
                _with3.Desc = desc;
                _with3.Id = (short)id;
                _with3.Importe = importe;
                decimal decImp = (decimal)(lDetRequest.ImpTrib + importe);
                lDetRequest.ImpTrib = (double)decImp;
            }
        }

        public void agregaOpcional(string id, string valor)
        {
            if (mFECAERequest.FeDetReq.Length > 0)
            {
                FECAEDetRequest lDetRequest = mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.GetUpperBound(0)];
                Opcional[] opcionales = lDetRequest.Opcionales;
                if (lDetRequest.Opcionales == null)
                {
                    Array.Resize(ref opcionales, 1);
                }
                else
                {
                    Array.Resize(ref opcionales, opcionales.Length + 1);
                }
                lDetRequest.Opcionales = opcionales;
                lDetRequest.Opcionales[lDetRequest.Opcionales.GetUpperBound(0)] = new Opcional();
                var _with4 = lDetRequest.Opcionales[lDetRequest.Opcionales.GetUpperBound(0)];
                _with4.Id = id;
                _with4.Valor = valor;
            }

        }

        public void agregaCbteAsoc(int tipo, int ptoVta, long nro)
        {
            if (mFECAERequest.FeDetReq.Length > 0)
            {
                FECAEDetRequest lDetRequest = mFECAERequest.FeDetReq[mFECAERequest.FeDetReq.GetUpperBound(0)];
                CbteAsoc[] cbteAsoc = lDetRequest.CbtesAsoc;
                if (lDetRequest.Opcionales == null)
                {
                    Array.Resize(ref cbteAsoc, 1);
                }
                else
                {
                    Array.Resize(ref cbteAsoc, cbteAsoc.Length + 1);
                }
                lDetRequest.CbtesAsoc = cbteAsoc;
                lDetRequest.CbtesAsoc[lDetRequest.CbtesAsoc.GetUpperBound(0)] = new CbteAsoc();
                var _with5 = lDetRequest.CbtesAsoc[lDetRequest.CbtesAsoc.GetUpperBound(0)];
                _with5.Nro = nro;
                _with5.PtoVta = ptoVta;
                _with5.Tipo = tipo;
            }
        }

        public async Task<(bool,string)> AutorizarAsync(int ptoVta, int tipoComp)
        {

            mCAEAInformarResponse = null;
            if ((mFECAERequest != null))
            {
                mFECAERequest.FeCabReq.PtoVta = ptoVta;
                mFECAERequest.FeCabReq.CbteTipo = tipoComp;
                fecaeSolicitarResponse = await getClient().FECAESolicitarAsync(mAuthRequest, mFECAERequest);
                if (isError(fecaeSolicitarResponse.Body.FECAESolicitarResult.Errors))
                {
                    return (false,fecaeSolicitarResponse.Body.FECAESolicitarResult.Errors[0].Msg);
                }

            }
            return (true,"Autorizada");
        }

        private bool isError(wsfev1.Err[] err)
        {
            if ((err == null) || (err.Length == 0))
            {
                return false;
            }
            else
            {
                mErrorCode = err[0].Code;
                mErrorDesc = err[0].Msg;
                return true;
            }
        }


        public AuthorizationResponse autorizarRespuestaV2(int index)
        {
            AuthorizationResponse response = null;
            
            DateTime vencimientoCae;

            if ((fecaeSolicitarResponse.Body.FECAESolicitarResult != null) && fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp.Length > index)
            {
                DateTime.TryParseExact(fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].CAEFchVto, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out vencimientoCae);
                response = new AuthorizationResponse
                {
                    CAE = fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].CAE,
                    VencimientoCae = vencimientoCae,
                    Resultado = fecaeSolicitarResponse.Body.FECAESolicitarResult.FeCabResp.Resultado
                };

                if(fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones!=null)
                {
                    if (fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones.Length > 0)
                    {
                        List<Observaciones> observaciones = new List<Observaciones>();
                        foreach (var obser in fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones)
                        {
                            observaciones.Add(new Observaciones
                            {
                                Codigo = obser.Code,
                                Descripcion = obser.Msg
                            });
                        }
                        response.Observaciones = observaciones;
                    }
                }


            }
            else if ((mCAEAInformarResponse != null) && mCAEAInformarResponse.FeDetResp.Length > index)
            {
                DateTime.TryParseExact(mCAEAInformarResponse.FeCabResp.FchProceso, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out vencimientoCae);
                response = new AuthorizationResponse
                {
                    CAE = mCAEAInformarResponse.FeDetResp[index].CAEA,
                    VencimientoCae = vencimientoCae,
                    Resultado = mCAEAInformarResponse.FeCabResp.Resultado
                };
            }

            return response;

        }
        public void autorizarRespuesta(int index, ref string cae, ref DateTime vencimiento, ref string resultado)
        {
            if ((fecaeSolicitarResponse.Body.FECAESolicitarResult != null) && fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp.Length > index)
            {
                cae = fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].CAE;
                DateTime.TryParseExact(fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].CAEFchVto, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out vencimiento);
                resultado = fecaeSolicitarResponse.Body.FECAESolicitarResult.FeCabResp.Resultado;
            }
            else if ((mCAEAInformarResponse != null) && mCAEAInformarResponse.FeDetResp.Length > index)
            {
                cae = mCAEAInformarResponse.FeDetResp[index].CAEA;
                DateTime.TryParseExact(mCAEAInformarResponse.FeCabResp.FchProceso, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out vencimiento);
                resultado = mCAEAInformarResponse.FeCabResp.Resultado;
            }
        }
        public string autorizarRespuestaObs(int index)
        {
            if ((fecaeSolicitarResponse.Body.FECAESolicitarResult != null) && fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp.Length > index && (fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones != null))
            {
                if (fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones.Length > 0)
                {
                    return fecaeSolicitarResponse.Body.FECAESolicitarResult.FeDetResp[index].Observaciones[0].Msg;
                }
            }
            else if ((mCAEAInformarResponse != null) && mCAEAInformarResponse.FeDetResp.Length > index && (mCAEAInformarResponse.FeDetResp[index].Observaciones != null))
            {
                if (mCAEAInformarResponse.FeDetResp[index].Observaciones.Length > 0)
                {
                    return mCAEAInformarResponse.FeDetResp[index].Observaciones[0].Msg;
                }
            }
            return "";
        }
        public async Task<bool> CAEASolicitar(int Periodo, short Orden, string CAE, System.DateTime FchVigDesd, System.DateTime FchVigHasta, System.DateTime FchTopeInf, System.DateTime FchProceso)
        {
            FECAEASolicitarResponse response = await getClient().FECAEASolicitarAsync(mAuthRequest, Periodo, Orden);

            bool Result = !isError(response.Body.FECAEASolicitarResult.Errors);
            if (Result)
            {
                CAE = response.Body.FECAEASolicitarResult.ResultGet.CAEA;
                FchVigDesd = System.DateTime.ParseExact(response.Body.FECAEASolicitarResult.ResultGet.FchVigDesde, dateFormat, null);
                FchVigHasta = System.DateTime.ParseExact(response.Body.FECAEASolicitarResult.ResultGet.FchVigHasta, dateFormat, null);
                FchTopeInf = System.DateTime.ParseExact(response.Body.FECAEASolicitarResult.ResultGet.FchTopeInf, dateFormat, null);
                FchProceso = System.DateTime.ParseExact(response.Body.FECAEASolicitarResult.ResultGet.FchProceso, dateFormat, null);
            }

            return Result;
        }
        public async Task<bool> CAEAConsultar(int Periodo, short Orden, string CAE, System.DateTime FchVigDesd, System.DateTime FchVigHasta, System.DateTime FchTopeInf, System.DateTime FchProceso)
        {
            FECAEAConsultarResponse response = await getClient().FECAEAConsultarAsync(mAuthRequest, Periodo, Orden);

            bool Result = !isError(response.Body.FECAEAConsultarResult.Errors);
            if (Result)
            {
                CAE = response.Body.FECAEAConsultarResult.ResultGet.CAEA;
                FchVigDesd = System.DateTime.ParseExact(response.Body.FECAEAConsultarResult.ResultGet.FchVigDesde, dateFormat, null);
                FchVigHasta = System.DateTime.ParseExact(response.Body.FECAEAConsultarResult.ResultGet.FchVigHasta, dateFormat, null);
                FchTopeInf = System.DateTime.ParseExact(response.Body.FECAEAConsultarResult.ResultGet.FchTopeInf, dateFormat, null);
                FchProceso = System.DateTime.ParseExact(response.Body.FECAEAConsultarResult.ResultGet.FchProceso.Substring(0, 8), dateFormat, null);
            }

            return Result;
        }

        public async Task<bool> CAEAInformar(int ptoVenta, int CbteTipo, string CAE)
        {

            //mFECAEResponse = null;

            FECAEARequest Req = new FECAEARequest();
            Req.FeCabReq = new FECAEACabRequest();
            Req.FeDetReq = (new List<FECAEADetRequest>()).ToArray();

            Req.FeCabReq.CantReg = mFECAERequest.FeDetReq.Length;
            Req.FeCabReq.PtoVta = ptoVenta;
            Req.FeCabReq.CbteTipo = CbteTipo;


            for (int I = 0; I <= mFECAERequest.FeDetReq.Length - 1; I++)
            {
                FECAEADetRequest[] requests = Req.FeDetReq;
                Array.Resize(ref requests, Req.FeDetReq.Length + 1);
                Req.FeDetReq = requests;
                FECAEADetRequest lDetalle = new FECAEADetRequest();
                Req.FeDetReq[Req.FeDetReq.Length - 1] = lDetalle;

                lDetalle.CAEA = CAE;
                lDetalle.Concepto = mFECAERequest.FeDetReq[I].Concepto;
                lDetalle.DocTipo = mFECAERequest.FeDetReq[I].DocTipo;
                lDetalle.DocNro = mFECAERequest.FeDetReq[I].DocNro;
                lDetalle.CbteDesde = mFECAERequest.FeDetReq[I].CbteDesde;
                lDetalle.CbteHasta = mFECAERequest.FeDetReq[I].CbteHasta;
                lDetalle.CbteFch = mFECAERequest.FeDetReq[I].CbteFch;
                lDetalle.ImpTotal = mFECAERequest.FeDetReq[I].ImpTotal;
                lDetalle.ImpTotConc = mFECAERequest.FeDetReq[I].ImpTotConc;
                lDetalle.ImpNeto = mFECAERequest.FeDetReq[I].ImpNeto;
                lDetalle.ImpOpEx = mFECAERequest.FeDetReq[I].ImpOpEx;
                lDetalle.ImpTrib = mFECAERequest.FeDetReq[I].ImpTrib;
                lDetalle.ImpIVA = mFECAERequest.FeDetReq[I].ImpIVA;
                lDetalle.FchServDesde = mFECAERequest.FeDetReq[I].FchServDesde;
                lDetalle.FchServHasta = mFECAERequest.FeDetReq[I].FchServHasta;
                lDetalle.FchVtoPago = mFECAERequest.FeDetReq[I].FchVtoPago;
                lDetalle.MonId = mFECAERequest.FeDetReq[I].MonId;
                lDetalle.MonCotiz = mFECAERequest.FeDetReq[I].MonCotiz;


                if ((mFECAERequest.FeDetReq[I].CbtesAsoc != null))
                {

                    for (int J = 0; J <= mFECAERequest.FeDetReq[I].CbtesAsoc.Length - 1; J++)
                    {
                        CbteAsoc lCompAsoc = new CbteAsoc();
                        CbteAsoc[] compAsocs = lDetalle.CbtesAsoc;
                        Array.Resize(ref compAsocs, lDetalle.CbtesAsoc.Length + 1);
                        lDetalle.CbtesAsoc = compAsocs;
                        lDetalle.CbtesAsoc[lDetalle.CbtesAsoc.Length - 1] = lCompAsoc;


                        lCompAsoc.Tipo = mFECAERequest.FeDetReq[I].CbtesAsoc[J].Tipo;
                        lCompAsoc.PtoVta = mFECAERequest.FeDetReq[I].CbtesAsoc[J].PtoVta;
                        lCompAsoc.Nro = mFECAERequest.FeDetReq[I].CbtesAsoc[J].Nro;
                    }
                }


                if ((mFECAERequest.FeDetReq[I].Tributos != null))
                {
                    mFECAERequest.FeDetReq[I].Tributos = (new List<Tributo>()).ToArray();


                    for (int J = 0; J <= mFECAERequest.FeDetReq[I].Tributos.Length - 1; J++)
                    {
                        Tributo lTributo = new Tributo();
                        Tributo[] tributos = lDetalle.Tributos;
                        Array.Resize(ref tributos, lDetalle.Tributos.Length + 1);
                        lDetalle.Tributos = tributos;
                        lDetalle.Tributos[lDetalle.Tributos.Length - 1] = lTributo;

                        lTributo.Id = mFECAERequest.FeDetReq[I].Tributos[J].Id;
                        lTributo.Desc = mFECAERequest.FeDetReq[I].Tributos[J].Desc;
                        lTributo.Alic = mFECAERequest.FeDetReq[I].Tributos[J].Alic;
                        lTributo.Importe = mFECAERequest.FeDetReq[I].Tributos[J].Importe;
                    }
                }


                if ((mFECAERequest.FeDetReq[I].Iva != null))
                {
                    mFECAERequest.FeDetReq[I].Iva = (new List<AlicIva>()).ToArray();

                    for (int J = 0; J <= mFECAERequest.FeDetReq[I].Iva.Length - 1; J++)
                    {
                        AlicIva lIva = new AlicIva();
                        AlicIva[] ivas = lDetalle.Iva;
                        Array.Resize(ref ivas, lDetalle.Iva.Length + 1);
                        lDetalle.Iva = ivas;
                        lDetalle.Iva[lDetalle.Iva.Length - 1] = lIva;
                        lIva.Id = mFECAERequest.FeDetReq[I].Iva[J].Id;
                        lIva.BaseImp = mFECAERequest.FeDetReq[I].Iva[J].BaseImp;
                        lIva.Importe = mFECAERequest.FeDetReq[I].Iva[J].Importe;
                    }
                }


                if ((mFECAERequest.FeDetReq[I].Opcionales != null))
                {
                    mFECAERequest.FeDetReq[I].Opcionales = (new List<Opcional>()).ToArray();


                    for (int J = 0; J <= mFECAERequest.FeDetReq[I].Opcionales.Length - 1; J++)
                    {
                        Opcional lOpcional = new Opcional();
                        Opcional[] opcionales = lDetalle.Opcionales;
                        Array.Resize(ref opcionales, lDetalle.Opcionales.Length + 1);
                        lDetalle.Opcionales = opcionales;
                        lDetalle.Opcionales[lDetalle.Opcionales.Length - 1] = lOpcional;
                        lOpcional.Id = mFECAERequest.FeDetReq[I].Opcionales[J].Id;
                        lOpcional.Valor = mFECAERequest.FeDetReq[I].Opcionales[J].Valor;
                    }
                }
            }

            var re = await getClient().FECAEARegInformativoAsync(mAuthRequest, Req);
            if (isError(re.Body.FECAEARegInformativoResult.Errors))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CAEASinMovimientoConsultar(int PtoVta, string CAEA, string Resultado)
        {
            Resultado = "";


            FECAEASinMovConsResponse response =await getClient().FECAEASinMovimientoConsultarAsync(mAuthRequest, CAEA, PtoVta);

            if (!isError(response.Errors))
            {

                for (int I = 0; I <= response.ResultGet.Length - 1; I++)
                {
                    if (!string.IsNullOrEmpty(Resultado))
                    {
                        Resultado = Resultado + Strings.Chr(10);
                    }
                    Resultado = Resultado + string.Format("{0}m, {1}, {2}", response.ResultGet[I].CAEA, response.ResultGet[I].FchProceso, response.ResultGet[I].PtoVta);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> CAEASinMovimientoInformar(int PtoVta, string CAEA, string Resultado)
        {
            FECAEASinMovimientoInformarResponse response = await getClient().FECAEASinMovimientoInformarAsync(mAuthRequest, PtoVta, CAEA);


            if (!isError(response.Body.FECAEASinMovimientoInformarResult.Errors))
            {
                Resultado = response.Body.FECAEASinMovimientoInformarResult.Resultado;

                return true;
            }

            return false;
        }

        public async Task<bool> CmpConsultar(int Tipo_cbte, int Punto_vta, long nro,  FECompConsultaResponse Cbte)
        {

            FECompConsultaReq request = new FECompConsultaReq();

            request.CbteTipo = Tipo_cbte;
            request.PtoVta = Punto_vta;
            request.CbteNro = nro;

            FECompConsultarResponse response = await getClient().FECompConsultarAsync(mAuthRequest, request);


            if (!isError(response.Body.FECompConsultarResult.Errors))
            {
                _Comp = response.Body.FECompConsultarResult;

                return true;
            }

            return false;
        }



        private async Task<ConsultaCuitResponse> InternoConsultaCUIT(long CUITConsulta)
        {
            ConsultaCuitResponse response = null;
            try
            {
                personaServiceA5.PersonaServiceA5Client awsClient;

                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.Transport;

                if (mModoProduccion)
                {
                    awsClient = new personaServiceA5.PersonaServiceA5Client(binding, new EndpointAddress(URLWSServiceA5));
                }
                else
                {
                    awsClient = new personaServiceA5.PersonaServiceA5Client(binding, new EndpointAddress(URLWSServiceA5_HOMO));
                }



                personaServiceA5.getPersonaResponse awsResult =  await awsClient.getPersonaAsync(mAuthRequest.Token, mAuthRequest.Sign, mAuthRequest.Cuit, CUITConsulta);
                
                response = new ConsultaCuitResponse(awsResult);

                return response;
            }
            catch (Exception e)
            {
                mErrorCode = -1;
                mErrorDesc = e.Message;
                return response;
            }

        }

        private long AddChecksum(int Prefix, double DNI)
        {
            string DNIStr = null;
            int Serie = 0;
            int I = 0;
            int Acc = 0;
            int Modulo = 0;

            DNIStr = Prefix.ToString() + DNI.ToString();
            Serie = 2;
            Acc = 0;

            for (I = DNIStr.Length - 1; I >= 0; I += -1)
            {
                Acc = Acc + Int32.Parse(DNIStr.Substring(I, 1)) * Serie;
                if (Serie == 7)
                {
                    Serie = 2;
                }
                else
                {
                    Serie = Serie + 1;
                }
            }
            Modulo = 11 - (Acc % 11);
            if (Modulo == 11)
            {
                Modulo = 0;
            }
            return long.Parse(DNIStr + Modulo.ToString());
        }

        public async Task<ConsultaCuitResponse> ConsultaCUIT(long CUITConsulta)
        {
            string CuitStr;
            CuitStr = CUITConsulta.ToString();
            bool ConsultaCUIT = false;
            ConsultaCuitResponse res = null;
            if (CuitStr.Length < 11)
            {

                res =  await InternoConsultaCUIT(AddChecksum(20, CUITConsulta));
                if (res==null)
                    res = await InternoConsultaCUIT(AddChecksum(27, CUITConsulta));

                if (res==null)
                    res = await InternoConsultaCUIT(AddChecksum(23, CUITConsulta));

                if (res==null)
                    res = await InternoConsultaCUIT(AddChecksum(24, CUITConsulta));

            }
            else
            {
                res = await InternoConsultaCUIT(CUITConsulta);
            }
            return  res;

        }
    }

    public class AuthorizationResponse
    {
        public string CAE { get; set; }
        public DateTime VencimientoCae { get; set; }
        public string Resultado { get; set; }
        public List<Observaciones> Observaciones { get; set; }
    }

    public class Observaciones
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
    }

}