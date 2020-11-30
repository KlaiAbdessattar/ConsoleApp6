using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JWT.Models
{
    public partial class Users
    {
        public Users()
        {
            CoworkingSpace = new HashSet<CoworkingSpace>();
            Formations = new HashSet<Formations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public int ProfilId { get; set; }

        public virtual Profil Profil { get; set; }
        public virtual ICollection<CoworkingSpace> CoworkingSpace { get; set; }
        public virtual ICollection<Formations> Formations { get; set; }
    }
}
