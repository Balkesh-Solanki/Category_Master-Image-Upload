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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryDbContext _context;

        public CategoryController(CategoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var category = await _context.Categories
                .Select(c => new CategoryResponseModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();

            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryResponseModel
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                })
                .FirstOrDefaultAsync();

                if (category == null)
                {
                    return NotFound("Category not found.");
                }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryRequestModel request)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(new CategoryResponseModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryRequestModel request)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound("Category not found.");

            category.CategoryName = request.CategoryName;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { id = category.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { Id = id });
        }
    }
}
