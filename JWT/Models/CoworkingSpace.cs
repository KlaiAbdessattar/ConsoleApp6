using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class CoworkingSpace
    {
        public CoworkingSpace()
        {
            SesionFormation = new HashSet<SesionFormation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
        public int Capacity { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int Mobile1 { get; set; }
        public int Mobile2 { get; set; }
        public int Mobile3 { get; set; }
        public string WebSite { get; set; }
        public double PriceOneHour { get; set; }
        public double PriceOneDay { get; set; }
        public double PriceOneMonth { get; set; }
        public double PriceOneYear { get; set; }
        public int Owner { get; set; }

        public virtual Users OwnerNavigation { get; set; }
        public virtual ICollection<SesionFormation> SesionFormation { get; set; }
    }
}
