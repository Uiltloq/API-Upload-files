using Microsoft.AspNetCore.Mvc;

namespace UploadFilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PostAsync(IFormFile formFile)
        {
            var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";

            Directory.CreateDirectory(uploadPath);

            string fullPath = Path.Combine(uploadPath, formFile.FileName);
            using (var fileStream = new FileStream(fullPath, FileMode.CreateNew))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return Ok();
        }
    }
}
