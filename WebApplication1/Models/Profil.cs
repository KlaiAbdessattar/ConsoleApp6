using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Profil
    {
        public Profil()
        {
            ProfileAction = new HashSet<ProfileAction>();
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfileAction> ProfileAction { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
