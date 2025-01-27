//using Category_Master.Data;
using Category_Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Category_Master.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageMstController : ControllerBase
    {
        private readonly CategoryDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageMstController(CategoryDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] FileUploadRequest file, [FromForm] int categoryId, [FromForm] int subCategoryId)
        {
            
                if (file == null || file.File.Length == 0)
                    return BadRequest("Invalid file.");

                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(file.File.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Only PNG, JPG, and JPEG files are allowed.");

                var uploadsPath = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.File.CopyToAsync(stream);
                }

                var image = new ImageMst
                {
                    CategoryId = categoryId,
                    SubCategoryId = subCategoryId,
                    ImageName = fileName,
                    ImageUrl = $"/images/{fileName}",
                    ImageType = fileExtension,
                    ImageSize = file.File.Length,
                    ImageMimetype = file.File.ContentType,
                    CreatedDate = DateTime.UtcNow
                };

                _context.ImageMsts.Add(image);
                await _context.SaveChangesAsync();
                return Ok(image);
        }

        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            var image = _context.ImageMsts.Find(id);
            if (image == null) return NotFound();

            return Ok(image);
        }

        [HttpGet]
        public IActionResult GetAllImages()
        {
            var images = _context.ImageMsts.ToList();
            return Ok(images);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] FileUploadRequest file, [FromForm] int categoryId, [FromForm] int subCategoryId)
        {
            if (file == null || file.File.Length == 0 || !(new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.File.FileName).ToLower())))
            {
                return BadRequest("Invalid image file.");
            }

            var existingImage = await _context.ImageMsts.FindAsync(id);
            if (existingImage == null)
            {
                return NotFound("Image not found.");
            }

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingImage.ImageUrl);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.File.FileName)}";
            var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", newFileName);

            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                await file.File.CopyToAsync(stream);
            }

            existingImage.ImageName = file.File.FileName;
            existingImage.ImageUrl = Path.Combine("images", newFileName);
            existingImage.ImageType = file.File.ContentType;
            existingImage.ImageSize = file.File.Length;
            existingImage.ImageMimetype = file.File.ContentType;
            existingImage.CategoryId = categoryId;
            existingImage.SubCategoryId = subCategoryId;
            existingImage.UpdatedDate = DateTime.Now;

            _context.ImageMsts.Update(existingImage);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Image updated successfully.", Image = existingImage });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _context.ImageMsts.Find(id);
            if (image == null) return NotFound();

            var imagePath = Path.Combine(_env.WebRootPath, image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.ImageMsts.Remove(image);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
