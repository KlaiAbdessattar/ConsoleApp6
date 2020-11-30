using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class FormationType
    {
        public FormationType()
        {
            Formations = new HashSet<Formations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int FormationSubTypeId { get; set; }

        public virtual FormationSubType FormationSubType { get; set; }
        public virtual ICollection<Formations> Formations { get; set; }
    }
}
