using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HandsOnAPIUsingImageUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();// reads form data asyncsch from the request
                var file = formCollection.Files.First(); // Retrieves 
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("{Download}")]
        public IActionResult Download(string fileName)
        {
            try
            {
                var folderName = "Resources/Images";
                var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

                if (!System.IO.File.Exists(pathToFile))
                {
                    return NotFound(); 
                }

                var fileBytes = System.IO.File.ReadAllBytes(pathToFile);// read as bytes
                var fileStream = new MemoryStream(fileBytes);

                
                var contentType = "application/octet-stream"; //generic binary type

                return File(fileStream, contentType, fileName);//downloadable response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}