using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ProfileAction
    {
        public int Id { get; set; }
        public int ProfilId { get; set; }
        public int ActionId { get; set; }

        public virtual Action Action { get; set; }
        public virtual Profil Profil { get; set; }
    }
}
