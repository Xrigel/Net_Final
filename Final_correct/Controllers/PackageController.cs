using Final_correct.data;
using Final_correct.DTOs;
using Final_correct.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PackageController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            if (_context.Packages == null)
            {
                return NotFound();
            }
            var Package = await _context.Packages.FindAsync(id);

            if (Package == null)
            {
                return NotFound();
            }

            return Package;
        }

        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(PackageDto categoryDto)
        {
            if (_context.Packages == null)
            {
                return Problem("Entity set 'AppDbContext.Category' is null.");
            }

            // Check if a package with the same name already exists
            if (_context.Packages.Any(p => p.Name == categoryDto.Name))
            {
                return Conflict("A package with the same name already exists.");
            }

            var package = new Package
            {
                Name = categoryDto.Name
            };

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Packages == null)
            {
                return NotFound();
            }
            var Package1 = await _context.Packages.FindAsync(id);
            if (Package1 == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(Package1);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
