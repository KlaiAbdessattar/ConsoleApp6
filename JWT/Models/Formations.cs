using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class Formations
    {
        public Formations()
        {
            SesionFormation = new HashSet<SesionFormation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MaxLimitNumberPlace { get; set; }
        public int Former { get; set; }
        public int FormationTypeId { get; set; }
        public int CorseId { get; set; }

        public virtual Corse Corse { get; set; }
        public virtual FormationType FormationType { get; set; }
        public virtual Users FormerNavigation { get; set; }
        public virtual ICollection<SesionFormation> SesionFormation { get; set; }
    }
}
