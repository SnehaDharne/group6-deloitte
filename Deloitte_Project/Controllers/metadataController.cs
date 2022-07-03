﻿using Microsoft.AspNetCore.Mvc;
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
    public class MetadataController : ControllerBase
    {
        private readonly CoreDbContext _context;
        public MetadataController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/<MetadataController>
        [HttpGet(nameof(GetMetadata))]
        [EnableCors("Policy1")]
        public async Task<ActionResult<String[]>> GetMetadata()
        {
            var sess = await _context.Metadatas.Where(c => c.Id > 0).ToListAsync();
            string[] result = new String[sess.Count];   
            for(int i=0; i<sess.Count; i++)
            {
                result[i] = sess[i].file_name.ToString();
            }
            return result;
        }
        //public async Task<ActionResult<String>> GetlastName()
        //{
        //    //var sess = await _context.Sessions.FindAsync(Id);
        //    var sess = await _context.Sessions.Where(c => c.isDeleted == false).ToListAsync();

        //    return sess[0].lastName;
        //}
    // GET api/<MetadataController>/5
        [EnableCors("Policy1")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<String[]>> GetCreatedBy(int Id)
        {
            var met = await _context.Metadatas.FindAsync(Id);
            if (met == null)
            {
                return NotFound();
            }
            
            else
            {

                string[] result = new String[3];

                result[0] = met.created_by.ToString();
                result[1] = met.created_on.ToString();
                result[2] = met.file_name.ToString();
                return result;

                
            }
        }

        //public async Task<ActionResult<Metadata>> GetCreatedOn(string file_name)
        //{
        //    var met = await _context.Metadatas.FindAsync(file_name);
        //    if (met == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(met.created_on);
        //    }
        //}

        // POST api/<MetadataController>
        [HttpPost]
        [EnableCors("Policy1")]
        public async Task<ActionResult<Metadata>> PostMetadata(Metadata metadata)
        {
            _context.Metadatas.Add(metadata);
            await _context.SaveChangesAsync();

            return Ok(_context.Metadatas);
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
            //metadata.isDeleted = true;
            //await _context.SaveChangesAsync();

            //return NoContent();
            return Ok(_context.Metadatas);
        }
    }
}
