using Microsoft.AspNetCore.Mvc;

namespace UploadFilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<ActionResult> PostAsync(IFormFile formFile)
        {

            var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";

            Directory.CreateDirectory(uploadPath);
            
            string fullPath = Path.Combine(uploadPath, formFile.FileName);
            try
            {
                using (var fileStream = new FileStream(fullPath, FileMode.CreateNew))
                {
                    await formFile.CopyToAsync(fileStream);
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }

            return Ok();
        }
    }
}
