using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_correct.Model;
using Final_correct.data;
using Final_correct.DTOs;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TypesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Type>> GetType(int id)
        {
          if (_context.Types == null)
          {
              return NotFound();
          }
            var type = await _context.Types.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Type>> PostType(TypeDto typedto)
        {
          if (_context.Types == null)
          {
              return Problem("Entity set 'AppDbContext.Types'  is null.");
          }
            var Type1= new Model.Type
            {
                Name = typedto.Name
            };
            _context.Types.Add(Type1);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            if (_context.Types == null)
            {
                return NotFound();
            }
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
