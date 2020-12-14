using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel;
using System.Globalization;
using System.Threading;
namespace Facturacion.AFIP
{

	//public class BIWSFEXV1
	//{

	//	private int mErrorCode;
	//	private string mErrorDesc;
	//	private wsfexv1.ClsFEXAuthRequest mAuthRequest = new wsfexv1.ClsFEXAuthRequest();
	//	private wsfexv1.ClsFEXRequest mFECAERequest;
	//	private wsfexv1.FEXResponseAuthorize mFECAEResponse;
	//	private const string URLWSAA = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
	//	private const string URLWSAA_HOMO = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
	//	private const string URLWSW = "https://servicios1.afip.gov.ar/wsfexv1/service.asmx";
	//	private const string URLWSW_HOMO = "https://wswhomo.afip.gov.ar/wsfexv1/service.asmx";
	//	private wsfexv1.ServiceSoapClient mClient;

	//	private bool mModoProduccion;

	//	public BIWSFEXV1()
	//	{
	//		CultureInfo newCulture = (CultureInfo) System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
	//		newCulture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
	//		newCulture.DateTimeFormat.DateSeparator = "";
	//		Thread.CurrentThread.CurrentCulture = newCulture;
	//	}

	//	private wsfexv1.ServiceSoapClient getClient()
	//	{

	//		if (mClient == null) {
	//			var binding = new BasicHttpBinding();
	//			binding.Security.Mode = BasicHttpSecurityMode.Transport;

	//			if (mModoProduccion) {
	//				mClient = new wsfexv1.ServiceSoapClient(binding, new EndpointAddress(URLWSW));
	//			} else {
	//				mClient = new wsfexv1.ServiceSoapClient(binding, new EndpointAddress(URLWSW_HOMO));
	//			}

	//		}
	//		return mClient;
	//	}

	//	public bool login(string certificadoPFX, string password, bool modoProduccion = false)
	//	{
	//		LoginTicket loginTicket = new LoginTicket();
	//		try {
	//			var url = modoProduccion ? URLWSAA : URLWSAA_HOMO;
	//			loginTicket.ObtenerLoginTicketResponse("wsfex", url, certificadoPFX, password);
	//			mAuthRequest = new wsfexv1.ClsFEXAuthRequest();
	//			mAuthRequest.Token = loginTicket.Token;
	//			mAuthRequest.Sign = loginTicket.Sign;
	//			mAuthRequest.Cuit = (long) loginTicket.CUIT;
	//			return true;
	//		} catch (Exception e) {
	//			mErrorCode = -1;
	//			mErrorDesc = e.Message;
	//			return false;
	//		}
	//	}

	//	public int ErrorCode {
	//		get { return mErrorCode; }
	//	}

	//	public string ErrorDesc {
	//		get { return mErrorDesc; }
	//	}

 //       public bool ModoProduccion
 //       {
 //           get { return mModoProduccion; }
 //           set
 //           {
 //               mModoProduccion = false;
 //               Interaction.MsgBox("La siguiente dll esta habilitada en modo Demo. Para obtener la licencia en produccion dirijase a contacto@bitingenieria.com.ar");
 //           }
 //       }

	//	public bool recuperaLastCMP(int ptoVta, int tipoComprobante, ref long nroComprobante)
	//	{
	//		wsfexv1.ClsFEX_LastCMP auth = new wsfexv1.ClsFEX_LastCMP();
	//		auth.Cbte_Tipo = (short) tipoComprobante;
	//		auth.Cuit = mAuthRequest.Cuit;
	//		auth.Pto_venta = (short) ptoVta;
	//		auth.Sign = mAuthRequest.Sign;
	//		auth.Token = mAuthRequest.Token;
	//		wsfexv1.FEXResponseLast_CMP result = getClient().FEXGetLast_CMP(auth);
	//		if (isError(result.FEXErr)) {
	//			return false;
	//		} else {
	//			nroComprobante = result.FEXResult_LastCMP.Cbte_nro;
	//			return true;
	//		}
	//	}

	//	public bool recuperaLastID(ref long Id)
	//	{
	//		wsfexv1.FEXResponse_LastID result = getClient().FEXGetLast_ID(mAuthRequest);
	//		if (isError(result.FEXErr)) {
	//			return false;
	//		} else {
	//			Id = result.FEXResultGet.Id;
	//			return true;
	//		}
	//	}

	//	public void reset()
	//	{
	//		mFECAERequest = new wsfexv1.ClsFEXRequest();
	//	}

	//	public void agregaFactura(long Id, DateTime Fecha_cbte, short Tipo_cbte, short Punto_vta, long Cbte_nro, short Tipo_expo, string Permiso_existente, short Dst_cmp, string Cliente, long Cuit_pais_cliente,
	//	string Domicilio_cliente, string Id_impositivo, string Moneda_Id, double Moneda_ctz, string Obs_comerciales, double Imp_total, string Obs, string Forma_pago, string Incoterms, string Incoterms_Ds,
	//	short Idioma_cbte)
	//	{
	//		if (mFECAERequest == null) {
	//			reset();
	//		}
	//		var _with1 = mFECAERequest;
	//		_with1.Cbte_nro = Cbte_nro;
	//		_with1.Cbte_Tipo = Tipo_cbte;
	//		_with1.Cliente = Cliente;
	//		_with1.Cuit_pais_cliente = Cuit_pais_cliente;
	//		_with1.Domicilio_cliente = Domicilio_cliente;
	//		_with1.Dst_cmp = Dst_cmp;
 //           _with1.Fecha_cbte = Fecha_cbte.ToString("yyyyMMdd");
	//		_with1.Forma_pago = Forma_pago;
	//		_with1.Id = Id;
	//		_with1.Id_impositivo = Id_impositivo;
	//		_with1.Idioma_cbte = Idioma_cbte;
	//		_with1.Imp_total = new Decimal(Imp_total);
	//		_with1.Incoterms = Incoterms;
	//		_with1.Incoterms_Ds = Incoterms_Ds;

	//		_with1.Moneda_ctz = new Decimal(Moneda_ctz);
	//		_with1.Moneda_Id = Moneda_Id;
	//		_with1.Obs = Obs;
	//		_with1.Obs_comerciales = Obs_comerciales;
	//		_with1.Permiso_existente = Permiso_existente;
	//		_with1.Punto_vta = Punto_vta;
 //           _with1.Tipo_expo = Tipo_expo;

	//	}

	//	public void agregaItem(string Pro_codigo, string Pro_ds, double Pro_qty, int Pro_umed, double Pro_precio_uni, double Pro_total_item, double Pro_bonificacion)
	//	{
	//		if ((mFECAERequest != null)) {
 //               Item[] items = mFECAERequest.Items;
	//			if (mFECAERequest.Items == null) {
	//				Array.Resize(ref items, 1);
	//			} else {
	//				Array.Resize(ref items, items.Length + 1);
	//			}
 //               mFECAERequest.Items = items;
	//			mFECAERequest.Items[mFECAERequest.Items.GetUpperBound(0)] = new FEAFIPLib.wsfexv1.Item();
	//			var _with2 = mFECAERequest.Items[mFECAERequest.Items.GetUpperBound(0)];
	//			_with2.Pro_bonificacion = new Decimal(Pro_bonificacion);
	//			_with2.Pro_codigo = Pro_codigo;
	//			_with2.Pro_ds = Pro_ds;
	//			_with2.Pro_precio_uni = new decimal(Pro_precio_uni);
	//			_with2.Pro_qty = new Decimal(Pro_qty);
	//			_with2.Pro_total_item = new Decimal(Pro_total_item);
	//			_with2.Pro_umed = Pro_umed;
	//		}
	//	}


	//	public void agregaPermiso(string Id_permiso, int Dst_merc)
	//	{
	//		if ((mFECAERequest != null)) {
 //               Permiso[] permisos = mFECAERequest.Permisos;
	//			if (mFECAERequest.Permisos == null) {
 //                   Array.Resize(ref permisos, 1);
	//			} else {
 //                   Array.Resize(ref permisos, permisos.Length + 1);
	//			}
 //               mFECAERequest.Permisos = permisos;
	//			mFECAERequest.Permisos[mFECAERequest.Permisos.GetUpperBound(0)] = new FEAFIPLib.wsfexv1.Permiso();
	//			var _with3 = mFECAERequest.Permisos[mFECAERequest.Permisos.GetUpperBound(0)];
	//			_with3.Dst_merc = Dst_merc;
	//			_with3.Id_permiso = Id_permiso;
	//		}
	//	}

	//	public void agregaCompAsoc(short Cbte_tipo, short Cbte_punto_vta, long Cbte_nro, long Cbte_cuit)
	//	{
	//		if ((mFECAERequest != null)) {
 //               Cmp_asoc[] cmps_asocs = mFECAERequest.Cmps_asoc;
	//			if (mFECAERequest.Cmps_asoc == null) {
 //                   Array.Resize(ref cmps_asocs, 1);
	//			} else {
 //                   Array.Resize(ref cmps_asocs, cmps_asocs.Length + 1);
	//			}
 //               mFECAERequest.Cmps_asoc = cmps_asocs;
	//			mFECAERequest.Cmps_asoc[mFECAERequest.Cmps_asoc.GetUpperBound(0)] = new FEAFIPLib.wsfexv1.Cmp_asoc();
	//			var _with4 = mFECAERequest.Cmps_asoc[mFECAERequest.Cmps_asoc.GetUpperBound(0)];
	//			_with4.Cbte_cuit = Cbte_cuit;
	//			_with4.Cbte_nro = Cbte_nro;
	//			_with4.Cbte_punto_vta = Cbte_punto_vta;
	//			_with4.Cbte_tipo = Cbte_tipo;
	//		}
	//	}

	//	public bool autorizar()
	//	{
	//		if ((mFECAERequest != null)) {
	//			mFECAEResponse = getClient().FEXAuthorize(mAuthRequest, mFECAERequest);
	//			if (isError(mFECAEResponse.FEXErr)) {
	//				return false;
	//			}

	//		}
	//		return true;
	//	}

	//	private bool isError(wsfexv1.ClsFEXErr err)
	//	{
	//		if ((err == null) || (err.ErrCode == 0)) {
	//			return false;
	//		} else {
	//			mErrorCode = err.ErrCode;
	//			mErrorDesc = err.ErrMsg;
	//			return true;
	//		}
	//	}

	//	public void autorizarRespuesta(ref string Cae, ref DateTime Fch_venc_Cae, ref string Resultado, ref string Reproceso)
	//	{
	//		if ((mFECAEResponse != null)) {
	//			Cae = mFECAEResponse.FEXResultAuth.Cae;
	//			DateTime.TryParseExact(mFECAEResponse.FEXResultAuth.Fch_venc_Cae, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out Fch_venc_Cae);
	//			Resultado = mFECAEResponse.FEXResultAuth.Resultado;
	//			Reproceso = mFECAEResponse.FEXResultAuth.Reproceso;
	//		}
	//	}

	//	public string autorizarRespuestaObs()
	//	{
	//		return mFECAEResponse.FEXResultAuth.Motivos_Obs;
	//	}

 //       public bool CmpConsultar(short Cbte_tipo, short Punto_vta, long nro, ref FEAFIPLib.wsfexv1.ClsFEXGetCMPR Cbte)
 //       {

 //           ClsFEXGetCMP request = new ClsFEXGetCMP();

 //           request.Cbte_tipo = Cbte_tipo;
 //           request.Punto_vta = Punto_vta;
 //           request.Cbte_nro = nro;

 //           FEXGetCMPResponse response = getClient().FEXGetCMP(mAuthRequest, request);


 //           if (!isError(response.FEXErr))
 //           {
 //               Cbte = response.FEXResultGet;

 //               return true;
 //           }

 //           return false;
 //       }


	//}
}
