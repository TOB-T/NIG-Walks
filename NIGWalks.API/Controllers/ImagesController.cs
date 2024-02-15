using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NIGWalks.API.Models.Domain;
using NIGWalks.API.Models.DTO;
using NIGWalks.API.Repositories;

namespace NIGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        // POSt: /api/images/Upload
        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto) 
        {
            ValidateFileUpload(imageUploadRequestDto);
            if(ModelState.IsValid)
            {
                // convert DTO to Domain model 
                var ImageDomainModel = new Image()
                {
                      File = imageUploadRequestDto.File,
                      FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                      FileSizeInBytes = imageUploadRequestDto.File.Length,
                      FileName = imageUploadRequestDto.FileName,
                      FileDescription = imageUploadRequestDto.FileDescription,

                };



                // User repository to upload image
                await _imageRepository.Upload(ImageDomainModel);
                return Ok(ImageDomainModel);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
