using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int ExpenseCatalogieId { get; set; }

        public virtual ExpenseCatalogue ExpenseCatalogie { get; set; }
    }
}
