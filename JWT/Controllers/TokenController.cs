using JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DEV_KLAIContext _context;

        public TokenController(IConfiguration config, DEV_KLAIContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Users _userData)
        {

            if (_userData != null && _userData.Name != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Name, _userData.Password);

                if (user != null)
                {
                    // //create claims details based on the user information
                    // var claims = new[] {
                    // new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    // new Claim("Id", user.Id.ToString()),
                    // new Claim("name", user.Name),
                    // new Claim("LastName", user.Lastname),
                    // new Claim("Email", user.Email),
                    //  new Claim("Passsword", user.Password),
                    //// new Claim("Email", user.Email)
                    //};

                    // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    // var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    // return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _userData.Name),
                    //new Claim(ClaimTypes.Role, "Manager")
                };

                    var tokeOptions = new JwtSecurityToken(
                       // issuer: true,
                       // audience: true,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Users> GetUser(string name, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
        }
    }
}