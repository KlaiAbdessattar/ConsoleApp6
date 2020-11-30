using ConsoleApp6.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ConsoleApp6.Services
{
    public interface IUserService
    {
        IList<Users> Find(WaterlineCriteria criteria);
        int Count(WaterlineCriteria criteria);

        public Users Get(int id);
    }
}
