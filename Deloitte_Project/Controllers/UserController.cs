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
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
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
        public async Task<ActionResult<User>> GetDetails(string Id, string password)
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(_context.Users);
        }

        // PUT api/<UserController>/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUser(string Id, User user)
        {
            var dbUser = await _context.Users.FindAsync(Id);
            if (dbUser == null)
            {
                return NotFound();
            }
            dbUser.firstName = user.firstName;
            dbUser.lastName = user.lastName;
            dbUser.age = user.age;
            dbUser.isDeleted = false;

            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            // Actually deletes entry
            //_context.Users.Remove(user);
            // Soft delete below
            user.isDeleted = true;
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok(_context.Users);
        }
    }
}
