using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Settlement
    {
        public Settlement()
        {
            Invoices = new HashSet<Invoices>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string Label { get; set; }

        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
