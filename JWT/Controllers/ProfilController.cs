using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilController : ControllerBase
    {
        private readonly DEV_KLAIContext _context;

        public ProfilController(DEV_KLAIContext context)
        {
            _context = context;
        }
        // GET: api/<ProfilController>
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profil>>> GetUsers()
        {
            return await _context.Profil.ToListAsync();
        }

        // GET api/<ProfilController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProfilController>
        [HttpPost]
        public async Task<ActionResult<Profil>> PostProfil(Profil profil)
        {
            _context.Profil.Add(profil);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetType), new { id = profil.Id }, profil);
        }

        // PUT api/<ProfilController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfilController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
