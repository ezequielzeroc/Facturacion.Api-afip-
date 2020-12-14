using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using System.IO;

namespace Facturacion.AFIP
{

    public class BIPadronARBA
    {
        public class ConsultaAlicuotaRespuesta
        {
            public int GrupoPercepcion;
            public int GrupoRetencion;
            public double AlicuotaPercepcion;
            public double AlicuotaRetencion;
        }

        private const string URLPADRON_TEST = "http://dfe.test.arba.gov.ar/DomicilioElectronico/SeguridadCliente/dfeServicioConsulta.do";
        private const string URLPADRON_PROD = "http://dfe.arba.gov.ar/DomicilioElectronico/SeguridadCliente/dfeServicioConsulta.do";

        public bool ModoProduccion;
        public string user;
        public string password;
        public string ErrorDesc;
        public ConsultaAlicuotaRespuesta ConsultaRespuesta;

        public string GetFileContent(string FechaDesde, string FechaHasta, double CUIT)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("CONSULTA-ALICUOTA");
            doc.AppendChild(root);
            root.AppendChild(doc.CreateElement("fechaDesde")).InnerText = FechaDesde;
            root.AppendChild(doc.CreateElement("fechaHasta")).InnerText = FechaHasta;
            root.AppendChild(doc.CreateElement("cantidadContribuyentes")).InnerText = "1";
            XmlNode contNode = root.AppendChild(doc.CreateElement("contribuyentes"));
            contNode.Attributes.Append(doc.CreateAttribute("class")).Value = "list";
            contNode.AppendChild(doc.CreateElement("contribuyente")).AppendChild(doc.CreateElement("cuitContribuyente")).InnerText = CUIT.ToString();
            return doc.OuterXml.Trim();
        }

        public string GetFileName(string FileContent)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(FileContent));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            string Filename = "DFEServicioConsulta_" + sBuilder.ToString() + ".xml"; 

            return Filename;
        }

        public bool ConsultaAlicuota(string FechaDesde, string FechaHasta, double CUIT)
        {
            Dictionary<string,object> parameters = new Dictionary<string,object>();
            parameters["user"] = user;
            parameters["password"] = password;

            string FileContent = GetFileContent(FechaDesde, FechaHasta, CUIT);

            FormUpload.FileParameter fileparam = new FormUpload.FileParameter(System.Text.Encoding.Default.GetBytes(FileContent), GetFileName(FileContent));
            parameters["file"] = fileparam;

            string URL = ModoProduccion ? URLPADRON_PROD : URLPADRON_TEST;
            HttpWebResponse response = FormUpload.MultipartFormDataPost(URL, "", parameters);
            if (response.StatusCode == HttpStatusCode.OK){
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.Default);
                    String responseString = reader.ReadToEnd();
                    try {
                        XmlDocument document = new XmlDocument();
                        document.LoadXml(responseString);
                        if (document.SelectSingleNode("//DFEError") != null)
                        {
                            ErrorDesc = document.SelectSingleNode("//DFEError//mensajeError").InnerText.Replace("<![CDATA[", "").Replace("]]/>", "");
                            return false;
                        }
                        else
                        {
                            ConsultaRespuesta = new ConsultaAlicuotaRespuesta();
                            double.TryParse(document.SelectSingleNode("//contribuyentes//contribuyente//alicuotaPercepcion").InnerText, out ConsultaRespuesta.AlicuotaPercepcion);
                            double.TryParse(document.SelectSingleNode("//contribuyentes//contribuyente//alicuotaRetencion").InnerText, out ConsultaRespuesta.AlicuotaRetencion);
                            int.TryParse(document.SelectSingleNode("//contribuyentes//contribuyente//grupoPercepcion").InnerText, out ConsultaRespuesta.GrupoPercepcion);
                            int.TryParse(document.SelectSingleNode("//contribuyentes//contribuyente//grupoRetencion").InnerText, out ConsultaRespuesta.GrupoRetencion);
                            return true;
                        }
                    } catch (Exception e) {
                        ErrorDesc = e.Message;
                        return false;
                    }
                }
            }
            return false;
        }

    }
}
