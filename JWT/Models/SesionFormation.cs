using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class SesionFormation
    {
        public SesionFormation()
        {
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int DayNumber { get; set; }
        public double HourNumber { get; set; }
        public int ParticipationNumberMax { get; set; }
        public int ParticipationNumberMin { get; set; }
        public string Localisation { get; set; }
        public string SessionStatut { get; set; }
        public string Description { get; set; }
        public bool Close { get; set; }
        public int InscriptionNumber { get; set; }
        public int NumberPresent { get; set; }
        public int Formation { get; set; }
        public int CowoekingPlace { get; set; }

        public virtual CoworkingSpace CowoekingPlaceNavigation { get; set; }
        public virtual Formations FormationNavigation { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
