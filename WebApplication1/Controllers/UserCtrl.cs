using ConsoleApp6.Services;
using ConsoleApp6.ServicesImpl;
using ConsoleApp6.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace ConsoleApp6.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserCtrl : ControllerBase
    {
        private readonly IUserService UserService;

        public UserCtrl(IUserService userService) : base()
        {
            UserService = userService;
        }


        // POST: api/user/find
        [HttpPost("find")]
        public ObjectResult Find([FromBody] WaterlineCriteria criteria)
        {
            List<UsersDTO> list = this.UserService.Find(criteria).Select(x => new UsersDTO(x, true)).ToList();
            criteria.limit = null;
            criteria.skip = null;
            int count = this.UserService.Count(criteria);

            return Ok(new ApiResponse(list, new { count = count }));
        }
        // GET: api/user/get
        [HttpGet("get")]
        public ObjectResult Get(int id)
        {
            UsersDTO user = new UsersDTO(this.UserService.Get(id));
            return Ok(new ApiResponse(user));
        }
    }
}
