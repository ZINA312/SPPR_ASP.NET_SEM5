using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_Kudosh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string _imagePath;

        public FilesController(IWebHostEnvironment webHost)
        {
            _imagePath = Path.Combine(webHost.WebRootPath, "Images");
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            if (file is null)
            {
                return BadRequest();
            }

            var filePath = Path.Combine(_imagePath, file.FileName);
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            using var fileStream = fileInfo.Create();
            await file.CopyToAsync(fileStream);

            var host = HttpContext.Request.Host;
            var fileUrl = $"Https://{host}/Images/{file.FileName}";
            return Ok(fileUrl);
        }
        [HttpDelete]
        public IActionResult DeleteFile(string fileName)
        {
            string path = Path.Combine(_imagePath, fileName);
            var fileInfo = new FileInfo(path);
            if (!string.IsNullOrEmpty(path) && fileInfo.Exists)
            {
                fileInfo.Delete();
                return Ok();
            }
            return BadRequest();
        }
    }
}
