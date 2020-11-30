using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class UsersDTO : DTO
    {
        public UsersDTO(Users user) : this(user, false)
        {

        }
        public UsersDTO(Users user, bool populateDependencies = false) : this()
        {
            this.Id = user.Id;
            this.Name = user.Name;
            if (populateDependencies == false)
            {
                this.profil = user.ProfilId;//if populatedependencie true, profil == profil


            }
            else
            {
                // this.profil = user.Profil != null ? new ProfilDTO(user.Profil) : null;

            }
        }

        public UsersDTO(Users user, string token) : this(user)
        {
            this.token = token;
        }

        public UsersDTO()
        {
            //actions = new Dictionary<string, ActionDTO>();
            //actions = new Dictionary<string, ProfilActionDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public object profil { get; set; }
        public string token { get; set; }
    }
}
