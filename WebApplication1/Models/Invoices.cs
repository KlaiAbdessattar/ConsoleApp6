using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int SettlementId { get; set; }

        public virtual Settlement Settlement { get; set; }
    }
}
