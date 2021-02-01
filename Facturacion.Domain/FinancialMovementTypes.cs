using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Domain
{
    public class FinancialMovementTypes
    {
        public int MovementTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FinancialMovements> FinancialMovements { get; set; }
    }
}
