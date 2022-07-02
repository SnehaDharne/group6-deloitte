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
    public class UserController : ControllerBase
    {
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private readonly CoreDbContext _context;
        public UserController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
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
        [EnableCors("Policy1")]
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
                if (Decrypt(user.password) == password)
                {
                    return user;

                }
                else
                    return BadRequest();
            }
        }

        // POST api/<UserController>
        [HttpPost]
        [EnableCors("Policy1")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (IsValidEmail(user.Id))
            {
                user.password = Encrypt(user.password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(_context.Users);
            }
            else
            {
                Console.WriteLine("Invalid Email Format");
                return BadRequest();
            }
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
            if (IsValidEmail(Id))
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
            else
            {
                Console.WriteLine("Invalid Email Format");
                return BadRequest();
            }
        }
    }
}