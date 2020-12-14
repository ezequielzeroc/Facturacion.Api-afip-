using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Data.Handlers
{
   public static class Helpers
    {
        public static double Truncate(this double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
        }
    }
}
