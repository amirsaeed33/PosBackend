using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.UI;
using Pos.Authorization;

namespace Pos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AbpAuthorize]
    public class FileUploadController : PosControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private const string ProfilePicturesFolder = "uploads/profile-pictures";
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public FileUploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            // Validate file size
            if (file.Length > MaxFileSize)
            {
                return BadRequest(new { message = "File size exceeds 5MB limit" });
            }

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !Array.Exists(allowedExtensions, ext => ext == fileExtension))
            {
                return BadRequest(new { message = "Invalid file type. Allowed types: JPG, JPEG, PNG, GIF, WebP" });
            }

            try
            {
                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(_hostingEnvironment.WebRootPath, ProfilePicturesFolder);
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return relative URL path
                var relativeUrl = $"/{ProfilePicturesFolder}/{fileName}";
                return Ok(new { url = relativeUrl, fileName = fileName });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"Error uploading file: {ex.Message}");
            }
        }

        [HttpDelete("delete-profile-picture")]
        public IActionResult DeleteProfilePicture([FromQuery] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest(new { message = "File name is required" });
            }

            try
            {
                var uploadsPath = Path.Combine(_hostingEnvironment.WebRootPath, ProfilePicturesFolder);
                var filePath = Path.Combine(uploadsPath, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok(new { message = "File deleted successfully" });
                }

                return NotFound(new { message = "File not found" });
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"Error deleting file: {ex.Message}");
            }
        }
    }
}

