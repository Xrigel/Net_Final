using Final_correct.data;
using Final_correct.DTOs;
using Final_correct.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var User1 = await _context.Users.FindAsync(id);

            if (User1 == null)
            {
                return NotFound();
            }

            return User1;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto userDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Category'  is null.");
            }
            var User1 = new User
            {
                Name = userDto.Name,
                LastName=userDto.LastName,
                UniqueNumber=userDto.UniqueNumber
            };
            _context.Users.Add(User1);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var User1 = await _context.Users.FindAsync(id);
            if (User1 == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User1);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
