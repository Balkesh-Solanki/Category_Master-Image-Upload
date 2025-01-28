//using Category_Master.Data;
using Category_Master.Models;
using Category_Master.RequestModels;
using Category_Master.ResponseModels;
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

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] ImageMstRequestModel request)

        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                return BadRequest("Invalid image file.");
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.ImageFile.CopyToAsync(stream);
            }

            var image = new ImageMst
            {
                CategoryId = request.CategoryId,
                SubCategoryId = request.SubCategoryId,
                ImageName = request.ImageFile.FileName,
                ImageUrl = $"/images/{fileName}",
                ImageType = Path.GetExtension(fileName),
                ImageSize = request.ImageFile.Length,
                ImageMimetype = request.ImageFile.ContentType,
                CreatedDate = DateTime.Now,
            };

            _context.ImageMsts.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new ImageMstResponseModel
            {
                Id = image.Id,
                ImageName = image.ImageName,
                ImageURL = image.ImageUrl,
                ImageType = image.ImageType,
                ImageSize = image.ImageSize,
                ImageMIMEType = image.ImageMimetype,
                CategoryId = image.CategoryId,
                SubCategoryId = image.SubCategoryId,
                CreatedDate = image.CreatedDate,
            });
        }

        [HttpGet("GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.ImageMsts
        .Where(i => i.Id == id)
        .Select(i => new ImageMstResponseModel
        {
            Id = i.Id,
            CategoryId = i.CategoryId,
            SubCategoryId = i.SubCategoryId,
            ImageName = i.ImageName,
            ImageURL = i.ImageUrl,
            ImageType = i.ImageType,
            ImageSize = i.ImageSize,
            ImageMIMEType = i.ImageMimetype,
            CreatedDate = i.CreatedDate,
            UpdateDate = i.UpdatedDate
        })
        .FirstOrDefaultAsync();

            if (image == null)
            {
                return NotFound("Image not found.");
            }

            return Ok(image);
        }

        [HttpGet("GetAllImages")]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.ImageMsts
            .Select(image => new ImageMstResponseModel
            {
                Id = image.Id,
                ImageName = image.ImageName,
                ImageURL = image.ImageUrl,
                ImageType = image.ImageType,
                ImageSize = image.ImageSize,
                ImageMIMEType = image.ImageMimetype,
                CategoryId = image.CategoryId,
                SubCategoryId = image.SubCategoryId,
                CreatedDate = image.CreatedDate,
                UpdateDate = image.UpdatedDate
            })
            .ToListAsync();

            return Ok(images);
        }

        [HttpPut("UpdateImage/{id}")]
        public async Task<IActionResult> UpdateImage(int id, [FromForm] ImageMstRequestModel request)
        {
            var existingImage = await _context.ImageMsts.FindAsync(id);
            if (existingImage == null)
            {
                return NotFound("Image not found.");
            }

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ImageFile.FileName)}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ImageFile.CopyToAsync(stream);
                }

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(existingImage.ImageUrl));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                existingImage.ImageName = request.ImageFile.FileName;
                existingImage.ImageUrl = $"/images/{fileName}";
                existingImage.ImageType = Path.GetExtension(fileName);
                existingImage.ImageSize = request.ImageFile.Length;
                existingImage.ImageMimetype = request.ImageFile.ContentType;
            }

            existingImage.CategoryId = request.CategoryId;
            existingImage.SubCategoryId = request.SubCategoryId;
            existingImage.UpdatedDate = DateTime.Now;

            _context.ImageMsts.Update(existingImage);
            await _context.SaveChangesAsync();

            return Ok(new { id = existingImage.Id });
        }

        [HttpDelete("DeleteImage/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.ImageMsts.FindAsync(id);
            if (image == null)
            {
                return NotFound("Image not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Path.GetFileName(image.ImageUrl));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.ImageMsts.Remove(image);
            await _context.SaveChangesAsync();

            return Ok(new { Id = id });
        }
    }
}
