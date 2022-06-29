using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Deloitte_Project.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deloitte_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CoreDbContext _context;
        // GET: api/<UserController>
        public UserController(CoreDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetUsers()
        {
            // return await _context.Contacts.ToListAsync();
            // Hide entries with IsDeleted = true
            return await _context.Users.Where(c => c.isDeleted == false).ToListAsync();
        }

       // public IEnumerable<string> Get()
       // {
       //     return new string[] { "value1", "value2" };
       // }

        // GET api/<UserController>/5


        [HttpGet("{Id}/{password}")]
        public async Task<ActionResult<user>> GetDetails(string Id, string password)
        {
            var user = await _context.Users.FindAsync(Id);
            //fix - make password null condition
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if(user.password == password)
                {
                    return user;

                }
                else 
                    return BadRequest();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
