using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class CorseCategorie
    {
        public CorseCategorie()
        {
            Corse = new HashSet<Corse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Corse> Corse { get; set; }
    }
}
