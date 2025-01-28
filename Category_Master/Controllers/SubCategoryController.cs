//using Category_Master.Data;
using Category_Master.DTO;
using Category_Master.Models;
using Category_Master.RequestModels;
using Category_Master.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Category_Master.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly CategoryDbContext _context;

        public SubCategoryController(CategoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetSubCategories()
        {
            var subCategory = await _context.SubCategories
                .Select(c => new SubCategoryResponseModel
                {
                    Id = c.Id,
                    SubCategoryName = c.SubCategoryName,
                    CategoryId = c.CategoryId
                })
                .ToListAsync();

            return Ok(subCategory);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubCategoryById(int id)
        {
            var subCategory = await _context.SubCategories
                .Where(c => c.Id == id)
                .Select(c => new SubCategoryResponseModel
                {
                    Id = c.Id,
                    SubCategoryName = c.SubCategoryName,
                    CategoryId = c.CategoryId
                })
                .FirstOrDefaultAsync();

            if (subCategory == null)
            {
                return NotFound("Sub Category not found.");
            }

            return Ok(subCategory);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubCategory([FromBody] SubCategoryRequestModel request)
        {
            var subCategory = new SubCategory
            {
                SubCategoryName = request.SubCategoryName,
                CategoryId = request.CategoryId
            };

            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(new SubCategoryResponseModel
            {
                Id = subCategory.Id,
                SubCategoryName = subCategory.SubCategoryName,
                CategoryId = subCategory.CategoryId
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubCategory(int id, [FromBody] SubCategoryRequestModel request)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
                return NotFound("Sub Category not found.");

            subCategory.SubCategoryName = request.SubCategoryName;
            subCategory.CategoryId = request.CategoryId;

            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { id = subCategory.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound("Sub Category not found.");
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return Ok(new { Id = id });
        }
    }
}
