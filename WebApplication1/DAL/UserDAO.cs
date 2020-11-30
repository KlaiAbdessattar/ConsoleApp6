using ConsoleApp6.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ConsoleApp6.DAL
{
    public class UserDAO
    {
        private DEV_KLAIContext Context;
        public UserDAO(DEV_KLAIContext context)
        {
            this.Context = context;
        }

        //FindList
        public IList<Users> Find(WaterlineCriteria criteria)
        {
            return criteria.Process(Context.Users);
        }
        public Users GetById(int id)
        {
           
                return this.Context.Users.Take(1).SingleOrDefault();
        }
    }
}
