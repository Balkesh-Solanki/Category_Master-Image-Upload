//using Category_Master.Data;
using Category_Master.DTO;
using Category_Master.Models;
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
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> GetSubCategoryById(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }
            return subCategory;
        }

        [HttpPost]
        public async Task<ActionResult<SubCategory>> AddSubCategory(SubCategoryDto subCategoryDto)
        {

            var subCategory = new SubCategory
            {
                SubCategoryName = subCategoryDto.SubCategoryName,
                CategoryId = subCategoryDto.CategoryId,
            };

            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(subCategory);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubCategory(int id, SubCategoryDto subCategoryDto)
        {

            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
                return NotFound("Sub Category not found.");

            subCategory.SubCategoryName = subCategoryDto.SubCategoryName;
            subCategory.CategoryId = subCategoryDto.CategoryId;

            _context.Entry(subCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = _context.SubCategories.Find(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
