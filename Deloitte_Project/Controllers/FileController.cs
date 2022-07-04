using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Deloitte_Project.Services;
using Microsoft.AspNetCore.Cors;

namespace Deloitte_Project.Controllers
{
    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        #region Property
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        #endregion
        #region Upload
        [HttpPost(nameof(Upload))]
        
        public IActionResult Upload([Required] List<IFormFile> formFiles)
        {
            string subDirectory;
            subDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName + "\\OUTPUT";
            try
            {
                _fileService.UploadFile(formFiles, subDirectory);

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Download File
        [HttpGet(nameof(Download))]
        public IActionResult Download([Required] string fileName)
        {
            string subDirectory;
            subDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName + "\\OUTPUT";
            var path = Path.Combine(subDirectory, fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pbix", Path.GetFileName(path));
        }
        #endregion
    }
}
