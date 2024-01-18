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
using Humanizer;

namespace Final_correct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PricesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
          if (_context.Prices == null)
          {
              return NotFound();
          }
            return await _context.Prices.ToListAsync();
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Price>> GetPrice(int id)
        {
          if (_context.Prices == null)
          {
              return NotFound();
          }
            var price = await _context.Prices.FindAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return price;
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(int id, Price price)
        {
            if (id != price.PriceId)
            {
                return BadRequest();
            }

            _context.Entry(price).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Price>> PostPrice(PriceDto dto)
        {
          if (_context.Prices == null)
          {
              return Problem("Entity set 'AppDbContext.Prices'  is null.");
          }
            var price = new Price
            {
                ProductPrice = dto.Price
            };
            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == dto.CategoryName);
                if (category != null)
                {
                    price.CategoryId = category.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.CategoryName}' not found.");
                }
            }
            if (!string.IsNullOrEmpty(dto.TypeName))
            {
                var type = _context.Types.FirstOrDefault(c => c.Name == dto.TypeName);
                if (type != null)
                {
                    price.TypeId = type.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.TypeName}' not found.");
                }
            }
            if (!string.IsNullOrEmpty(dto.PackageName))
            {
                var package = _context.Packages.FirstOrDefault(c => c.Name == dto.PackageName);
                if (package != null)
                {
                    price.PackageId = package.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.PackageName}' not found.");
                }
            }
            
            _context.Prices.Add(price);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice(int id)
        {
            if (_context.Prices == null)
            {
                return NotFound();
            }
            var price = await _context.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _context.Prices.Remove(price);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriceExists(int id)
        {
            return (_context.Prices?.Any(e => e.PriceId == id)).GetValueOrDefault();
        }
    }
}
