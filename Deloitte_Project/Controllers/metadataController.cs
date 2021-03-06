using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Deloitte_Project.Models;
using Microsoft.AspNetCore.Cors;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deloitte_Project.Controllers
{
    [EnableCors("Policy1")]
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
        [HttpGet(nameof(GetMetadataFileNames))]
        public async Task<ActionResult<String[]>> GetMetadataFileNames(string username)
        {
            var sess = await _context.Metadatas.Where(c => c.userid == username).ToListAsync();
            string[] result = new String[sess.Count];   
            for(int i=0; i<sess.Count; i++)
            {
                result[i] = sess[i].file_name.ToString();
            }
            return result;
        }

        // GET: api/<MetadataController>
        [HttpGet(nameof(GetMetadataIds))]
        public async Task<ActionResult<String[]>> GetMetadataIds(string username)
        {
            var sess = await _context.Metadatas.Where(c => c.userid == username).ToListAsync();
            string[] result = new String[sess.Count];
            for (int i = 0; i < sess.Count; i++)
            {
                result[i] = sess[i].Id.ToString();
            }
            return result;
        }

        // GET api/<MetadataController>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<String[]>> GetCreatedBy(int Id)
        {
            var met = await _context.Metadatas.FindAsync(Id);
            if (met == null) {
                return NotFound();
            } else {
                string[] result = new String[3];
                if (met.created_by == null) {
                    result[0] = "CREATED_BY";
                } else {
                    result[0] = met.created_by.ToString();
                }

                if (met.created_by == null) {
                    result[1] = "CREATED_ON";
                } else {
                    result[1] = met.created_on.ToString();
                }

                if (met.created_by == null) {
                    result[2] = "FILE_NAME";
                } else {
                    result[2] = met.file_name.ToString();
                }

                return result;

                
            }
        }

        // POST api/<MetadataController>
        [HttpPost]
        public async Task<ActionResult<Metadata>> PostMetadata(Metadata metadata)
        {
            _context.Metadatas.Add(metadata);
            await _context.SaveChangesAsync();

            return Ok(_context.Metadatas);
        }

        // PUT api/<MetadataController>/5
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser(string username, string fullname)
        {
            var sess = await _context.Metadatas.Where(c => c.userid == username).ToListAsync();
            for (int i = 0; i < sess.Count; i++)
            {
                sess[i].created_by = fullname;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<MetadataController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetadata(int Id)
        {
            var metadata = await _context.Metadatas.FindAsync(Id);
            if (metadata == null)
            {
                return NotFound();
            }

            // Actually deletes entry
            //_context.Metadatas.Remove(metadata);

            // Soft delete below
            //metadata.isDeleted = true;
            //await _context.SaveChangesAsync();

            //return NoContent();
            return Ok(_context.Metadatas);
        }
    }
}
