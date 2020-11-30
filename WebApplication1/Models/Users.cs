using System;
using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.Models
{
    public partial class Users : Model
    {
        public Users(UsersDTO userDTO) : this()
        {
            this.Id = userDTO.Id;
            this.Name = userDTO.Name;
            // this.ProfilId = Utilities.GetIdFromNavigationPoperty(userDTO.profil);
        }

        public Users()
        {
            CoworkingSpace = new HashSet<CoworkingSpace>();
            Formations = new HashSet<Formations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfilId { get; set; }

        public virtual Profil Profil { get; set; }
        public virtual ICollection<CoworkingSpace> CoworkingSpace { get; set; }
        public virtual ICollection<Formations> Formations { get; set; }

        // il manque deux champs de dat create et update 
    }
}
