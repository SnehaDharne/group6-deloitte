using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deloitte_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class metadataController : ControllerBase
    {
        // GET: api/<metadata>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<metadata>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<metadata>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<metadata>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<metadata>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
