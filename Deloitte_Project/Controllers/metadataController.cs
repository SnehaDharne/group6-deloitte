using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Deloitte_Project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deloitte_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public MetadataController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/<MetadataController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metadata>>> GetMetadata()
        {
            return await _context.Metadata.Where(c => c.isDeleted == false).ToListAsync();
        }

        // GET api/<MetadataController>/5
        [EnableCors("Policy1")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Metadata>> GetDetails(string Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<MetadataController>
        [HttpPost]
        [EnableCors("Policy1")]
        public async Task<ActionResult<Metadata>> PostMetadata(Metadata metadata)
        {
            _context.Metadata.Add(metadata);
            await _context.SaveChangesAsync();

            return Ok(_context.Metadata);
        }

        // PUT api/<MetadataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MetadataController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetadata(string Id)
        {
            var metadata = await _context.Users.FindAsync(Id);
            if (metadata == null)
            {
                return NotFound();
            }

            // Actually deletes entry
            //_context.Users.Remove(user);

            // Soft delete below
            metadata.isDeleted = true;
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok(_context.Metadata);
        }
    }
}
