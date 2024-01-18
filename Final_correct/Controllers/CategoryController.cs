using Final_correct.data;
using Final_correct.DTOs;
using Final_correct.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var Category = await _context.Categories.FindAsync(id);

            if (Category == null)
            {
                return NotFound();
            }

            return Category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDto CategoryDto)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDbContext.Category'  is null.");
            }
            var Category1 = new Category
            {
                Name = CategoryDto.Name
            };
            _context.Categories.Add(Category1);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var Category = await _context.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
