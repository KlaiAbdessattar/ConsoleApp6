using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class PurchaseOrder
    {
        public int Id { get; set; }
        public int SettlementId { get; set; }
        public int SessionFormationId { get; set; }

        public virtual SesionFormation SessionFormation { get; set; }
        public virtual Settlement Settlement { get; set; }
    }
}
