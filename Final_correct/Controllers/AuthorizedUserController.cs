using Final_correct.data;
using Final_correct.DTOs;
using Final_correct.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizedUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorizedUserController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorizedUser>> GetAuthorizedUser(int id)
        {
            if (_context.AuthorizedUsers == null)
            {
                return NotFound();
            }
            var AuthorizedUser1 = await _context.AuthorizedUsers.FindAsync(id);

            if (AuthorizedUser1 == null)
            {
                return NotFound();
            }

            return AuthorizedUser1;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorizedUser>> PosthorizedUser(AuthorizedUserDto AuthorizedUserDto)
        {
            if (_context.AuthorizedUsers == null)
            {
                return Problem("Entity set 'AppDbContext.Category'  is null.");
            }
            var AuthorizedUser1 = new AuthorizedUser
            {
                Name = AuthorizedUserDto.Name
            };
            _context.AuthorizedUsers.Add(AuthorizedUser1);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorizedUser(int id)
        {
            if (_context.AuthorizedUsers == null)
            {
                return NotFound();
            }
            var AuthorizedUser1 = await _context.AuthorizedUsers.FindAsync(id);
            if (AuthorizedUser1 == null)
            {
                return NotFound();
            }

            _context.AuthorizedUsers.Remove(AuthorizedUser1);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
