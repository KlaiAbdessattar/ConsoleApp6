using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class Corse
    {
        public Corse()
        {
            Formations = new HashSet<Formations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CorseCatalogueId { get; set; }

        public virtual CorseCategorie CorseCatalogue { get; set; }
        public virtual ICollection<Formations> Formations { get; set; }
    }
}
