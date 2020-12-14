using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    //0003 0,00 %
    //0004 10,50 %
    //0005 21,00 %
    //0006 27,00 %
    //0008 5,00 %
    //0009 2,50 %
    public class Taxes
    {
        public int TaxId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
    }
}
