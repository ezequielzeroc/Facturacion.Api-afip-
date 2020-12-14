using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.ServiceModel;
using Facturacion.AFIP.ServiceReference1;
using System.Threading.Tasks;

namespace Facturacion.AFIP
{
    public class WscdcClass
    {
        private const string dateFormat = "yyyyMMdd";
        private int mErrorCode;
        private string mErrorDesc;
        private CmpAuthRequest mAuthRequest;
        private const string URLWSAA = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string URLWSAA_HOMO = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string URLWSW = "https://servicios1.afip.gov.ar/wscdc/service.asmx";
        private const string URLWSW_HOMO = "https://wswhomo.afip.gov.ar/WSCDC/service.asmx";
        private bool mModoProduccion;
        private string mCertificadoPFX;
        private string mPassword;

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

        public WscdcClass()
        {
//            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
//            newCulture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
//            newCulture.DateTimeFormat.DateSeparator = "";
//            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        public bool login(string certificadoPFX, string password)
        {
            string servicio = "wscdc";
            mCertificadoPFX = certificadoPFX;
            mPassword = password;
            LoginTicket loginTicket = new LoginTicket();
            try
            {
                var url = mModoProduccion ? URLWSAA : URLWSAA_HOMO;
                loginTicket.ObtenerLoginTicketResponse(servicio, url, certificadoPFX, password);
                mAuthRequest = new CmpAuthRequest();
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

        private ServiceReference1.ServiceSoapClient getClient()
        {

                var binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.Transport;

                if (mModoProduccion)
                {
                    return new ServiceReference1.ServiceSoapClient(binding, new EndpointAddress(URLWSW));
                }
                else
                {
                    return new ServiceReference1.ServiceSoapClient(binding, new EndpointAddress(URLWSW_HOMO));
                }
        }

        public async Task<bool> ComprobanteConstatar(string CbteModo, long CuitEmisor, int PtoVta,
          int CbteTipo, long CbteNro, string CbteFch, double ImpTotal,
          string CodAutorizacion, string DocTipoReceptor, string DocNroReceptor)
        {


            mErrorCode = 0;
            mErrorDesc = "";

            CmpDatos CmpReq = new CmpDatos();
            CmpReq.CbteModo = CbteModo;
            CmpReq.CuitEmisor = CuitEmisor;
            CmpReq.PtoVta = PtoVta;
            CmpReq.CbteTipo = CbteTipo;
            CmpReq.CbteNro = CbteNro;
            CmpReq.CbteFch = CbteFch;
            CmpReq.ImpTotal = ImpTotal;
            CmpReq.CodAutorizacion = CodAutorizacion;
            CmpReq.DocTipoReceptor = DocTipoReceptor;
            CmpReq.DocNroReceptor = DocNroReceptor;
            ServiceReference1.ServiceSoapClient wscdc = getClient();
            var Resultado =  await wscdc.ComprobanteConstatarAsync(mAuthRequest, CmpReq);
            if (Resultado.Body.ComprobanteConstatarResult.Errors != null && (Resultado.Body.ComprobanteConstatarResult.Errors.Length > 0))
            {
                mErrorCode = Resultado.Body.ComprobanteConstatarResult.Errors[0].Code;
                mErrorDesc = Resultado.Body.ComprobanteConstatarResult.Errors[0].Msg;
            }
            else
            {
                if (Resultado.Body.ComprobanteConstatarResult.Resultado == "A")
                {
                    return true;
                }
                else if ((Resultado.Body.ComprobanteConstatarResult.Observaciones != null) && (Resultado.Body.ComprobanteConstatarResult.Observaciones.Length > 0))
                {
                    mErrorCode = Resultado.Body.ComprobanteConstatarResult.Observaciones[0].Code;
                    mErrorDesc = Resultado.Body.ComprobanteConstatarResult.Observaciones[0].Msg;
                }
                else
                {
                    mErrorCode = -1;
                    mErrorDesc = "Error Desconocido";
                }
            }
            return false;

        }



    }
}
