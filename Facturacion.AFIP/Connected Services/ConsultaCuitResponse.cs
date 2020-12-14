using FEAFIPLib;
using System.Threading.Tasks;

public class ConsultaCuitResponse
{

    //private string[] CondicionesIVA = { "Monotributo", "Responsable Inscripto", "IVA Exento", "Consumidor Final" };
    private int[] CondicionesIVA = { 6, 2, 4, 5};
    private Facturacion.AFIP.personaServiceA5.getPersonaResponse _response;
    private bool FindTax(int TaxId)
    {
        if(_response.personaReturn.datosMonotributo!=null)
        {
            for (int I = 0; (I <= (_response.personaReturn.datosMonotributo.impuesto.Length - 1)); I++)
            {
                if (((_response.personaReturn.datosMonotributo.impuesto[I].idImpuesto == TaxId)))
                {
                    return true;
                }
            }
        }
        if (_response.personaReturn.datosRegimenGeneral != null)
        {
            for (int I = 0; (I<= (_response.personaReturn.datosRegimenGeneral.impuesto.Length- 1)); I++)
            {
                if (((_response.personaReturn.datosRegimenGeneral.impuesto[I].idImpuesto == TaxId)))
                {
                    return true;

                }

            }

        }
        return false;

    }

    public  Facturacion.AFIP.personaServiceA5.personaReturn test()
    {
        return _response.personaReturn;
    }

    public ConsultaCuitResponse(Facturacion.AFIP.personaServiceA5.getPersonaResponse responseObj)
    {
        _response = responseObj;
    }

    public long idPersona
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.idPersona;
            }
            catch 
            {

                return _response.personaReturn.errorConstancia.idPersona;
            }
        }
    }

    public string tipoPersona
    {
        get
        {
            return _response.personaReturn.datosGenerales != null ? _response.personaReturn.datosGenerales.tipoPersona : "";
        }
    }

    public string nombre2
    {
        get
        {
            if(_response.personaReturn.datosGenerales!=null)
            {
                return _response.personaReturn.datosGenerales.nombre;
            }
            else
            {
                return _response.personaReturn.errorConstancia.nombre;
            }
        }
    }

    public string apellido2
    {
        get
        {
            if (_response.personaReturn.datosGenerales != null)
            {
                return _response.personaReturn.datosGenerales.apellido;
            }
            else
            {
                return _response.personaReturn.errorConstancia.apellido;
            }
        }
    }
    public string nombre
    {
        get
        {
            if (_response.personaReturn.datosGenerales!=null)
            {
                if ((tipoPersona == "FISICA"))
                {
                    return (_response.personaReturn.datosGenerales.apellido + (" " + _response.personaReturn.datosGenerales.nombre));
                }
                else
                {
                    return _response.personaReturn.datosGenerales.razonSocial;
                }
            }
            else
            {
                return _response.personaReturn.errorConstancia.apellido + " " + _response.personaReturn.errorConstancia.nombre;
            }

        }
    }
    
    public string RazonSocial
    {
        get
        {
            return _response.personaReturn.datosGenerales.razonSocial;
        }
    }
    public string tipoDocumento
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.tipoClave;
            }
            catch
            {
                return "";
            }

        }
    }

    public long numeroDocumento
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.idPersona;
            }
            catch
            {
                return 0;
            }

        }
    }

    public string domicilioFiscal_direccion
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.domicilioFiscal.direccion /*_response.domicilio[0].direccion*/;
            }
            catch
            {
                return "";
            }

        }
    }

    public string domicilioFiscal_localidad
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.domicilioFiscal.localidad;
            }
            catch
            {
                return "";
            }

        }
    }

    public string domicilioFiscal_codPostal
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.domicilioFiscal.codPostal;
            }
            catch
            {
                return "";
            }

        }
    }

    public int domicilioFiscal_idProvincia
    {
        get
        {
            try
            {
                return _response.personaReturn.datosGenerales.domicilioFiscal.idProvincia;
            }
            catch
            {
                return 0;
            }

        }
    }

    public int condicionIVA
    {
        get
        {
            if (FindTax(20))
            {
                return 0;
                //  Monotributo
            }
            else if (this.FindTax(30))
            {
                return 1;
                //  Responsable inscripto
            }
            else if (this.FindTax(32))
            {
                return 2;
                //  IVA Exento
            }
            else
            {
                return 3;
                //  Consumidor final
            }

        }
    }

    //public string condicionIVADesc
    //{
    //    get
    //    {
    //        return CondicionesIVA[this.condicionIVA];
    //    }
    //}

    public int condicionIVAID
    {
        get
        {
            return CondicionesIVA[this.condicionIVA];
        }
    }
}



//using FEAFIPLib;
//public class ConsultaCuitResponse
//{

//    private string[] CondicionesIVA = { "Monotributo", "Responsable Inscripto", "IVA Exento", "Consumidor Final" };

//    private FEAFIPLib.ServiceA4.persona _response;

//    private bool FindTax(int TaxId)
//    {
//        if (!(_response.impuesto == null))
//        {
//            for (int I = 0; (I
//                        <= (_response.impuesto.Length - 1)); I++)
//            {
//                if (((_response.impuesto[I].idImpuesto == TaxId)
//                            && (_response.impuesto[I].estado == "ACTIVO")))
//                {
//                    return true;

//                }

//            }

//        }
//        return false;

//    }

//    public ConsultaCuitResponse(FEAFIPLib.ServiceA4.persona responseObj)
//    {
//        _response = responseObj;
//    }

//    public long idPersona
//    {
//        get
//        {
//            return _response.idPersona;
//        }
//    }

//    public string tipoPersona
//    {
//        get
//        {
//            return _response.tipoPersona;
//        }
//    }

//    public string nombre
//    {
//        get
//        {
//            if ((tipoPersona == "FISICA"))
//            {
//                return (_response.apellido + (" " + _response.nombre));
//            }
//            else
//            {
//                return _response.razonSocial;
//            }

//        }
//    }

//    public string tipoDocumento
//    {
//        get
//        {
//            try
//            {
//                return _response.tipoDocumento;
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }

//    public string numeroDocumento
//    {
//        get
//        {
//            try
//            {
//                return _response.numeroDocumento;
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }

//    public string domicilioFiscal_direccion
//    {
//        get
//        {
//            try
//            {
//                return _response.domicilio[0].direccion;
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }

//    public string domicilioFiscal_localidad
//    {
//        get
//        {
//            try
//            {
//                return _response.domicilio[0].localidad;
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }

//    public string domicilioFiscal_codPostal
//    {
//        get
//        {
//            try
//            {
//                return _response.domicilio[0].codPostal;
//            }
//            catch
//            {
//                return "";
//            }

//        }
//    }

//    public int domicilioFiscal_idProvincia
//    {
//        get
//        {
//            try
//            {
//                return _response.domicilio[0].idProvincia;
//            }
//            catch
//            {
//                return 0;
//            }

//        }
//    }

//    public int condicionIVA
//    {
//        get
//        {
//            if (this.FindTax(20))
//            {
//                return 0;
//                //  Monotributo
//            }
//            else if (this.FindTax(30))
//            {
//                return 1;
//                //  Responsable inscripto
//            }
//            else if (this.FindTax(32))
//            {
//                return 2;
//                //  IVA Exento
//            }
//            else
//            {
//                return 3;
//                //  Consumidor final
//            }

//        }
//    }

//    public string condicionIVADesc
//    {
//        get
//        {
//            return CondicionesIVA[this.condicionIVA];
//        }
//    }
//}