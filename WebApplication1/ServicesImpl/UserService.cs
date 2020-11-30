using ConsoleApp6.DAL;
using ConsoleApp6.Services;
using ConsoleApp6.ServicesImpl;
using ConsoleApp6.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ConsoleApp6.ServicesImpl
{
    public class UserService : IUserService
    {
        private UserDAO UserDAO;
        public UserService(UserDAO userDAO)
        {
            UserDAO = userDAO;
        }

        //FindList
        public IList<Users> Find(WaterlineCriteria criteria)
        {
            return UserDAO.Find(criteria);
        }

        //count
        public int Count(WaterlineCriteria criteria)
        {
            return Find(criteria).Count;
        }

        public Users Get(int id)
        {
            return UserDAO.GetById(id);
        }



    }
}
