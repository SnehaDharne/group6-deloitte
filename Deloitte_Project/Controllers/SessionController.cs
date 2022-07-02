using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Deloitte_Project.Models;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Net.Mail;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deloitte_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Policy1")]
    public class SessionController : ControllerBase
    {

        private readonly CoreDbContext _context;
        public SessionController(CoreDbContext context)
        {
            _context = context;
        }
        // GET: api/<SessionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            // return await _context.Contacts.ToListAsync();
            // Hide entries with IsDeleted = true
            return await _context.Sessions.Where(c => c.isDeleted == false).ToListAsync();
        }
        // GET api/<SessionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SessionController>
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(Session sess)
        {
           
               
                _context.Sessions.Add(sess);
                await _context.SaveChangesAsync();

                return Ok(_context.Sessions);
          
        }

        // PUT api/<SessionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SessionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(string Id)

        {
            var sess = await _context.Sessions.FindAsync(Id);
            _context.Sessions.Remove(sess);
            return Ok(_context.Sessions);
        }
    }
}
