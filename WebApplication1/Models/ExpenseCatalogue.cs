using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ExpenseCatalogue
    {
        public ExpenseCatalogue()
        {
            Expense = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expense { get; set; }
    }
}
