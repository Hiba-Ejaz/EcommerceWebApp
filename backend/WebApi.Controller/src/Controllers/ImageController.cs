using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        // private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
            // _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string productId)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("No file uploaded");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                var image = new Image
                {
                    ImageData = imageBytes,
                    ProductId = productId.ToString()
                };
                var uploadedImage = await _imageService.UploadImageAsync(image);
                return Ok(new { imageId = uploadedImage.Id });  // Return the image ID
            }
        }
    }
}