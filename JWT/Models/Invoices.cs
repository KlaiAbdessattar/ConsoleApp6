using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int SettlementId { get; set; }

        public virtual Settlement Settlement { get; set; }
    }
}
