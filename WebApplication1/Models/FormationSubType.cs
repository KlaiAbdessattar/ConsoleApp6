using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class FormationSubType
    {
        public FormationSubType()
        {
            FormationType = new HashSet<FormationType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FormationType> FormationType { get; set; }
    }
}
