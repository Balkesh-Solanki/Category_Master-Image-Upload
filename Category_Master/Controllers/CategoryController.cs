//using Category_Master.Data;
using Category_Master.DTO;
using Category_Master.Models;
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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(CategoryDto categoryDto)
        {
            var categories = new Category
            {
                CategoryName = categoryDto.CategoryName,
            };

            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();
            return Ok(categories);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
                return NotFound("User not found.");

            categories.CategoryName = categoryDto.CategoryName;

            _context.Entry(categories).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
