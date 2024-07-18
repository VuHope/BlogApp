using BlogAppMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogAppMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository imagesRepository;

        public ImagesController(IImagesRepository imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //Call reposotory to save image
            var imageURL = await imagesRepository.UploadAsync(file);
            if(imageURL == null)
            {
                return Problem("Image upload failed", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });
        }
    }
}
