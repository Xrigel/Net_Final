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
    public class InsuranceProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InsuranceProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InsuranceProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceProduct>>> GetInsuranceProducts()
        {
            if (_context.InsuranceProducts == null)
            {
                return NotFound();
            }
            return await _context.InsuranceProducts.ToListAsync();
        }

        // GET: api/InsuranceProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceProductDto>> GetInsuranceProduct(int id)
        {
            if (_context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProduct = await _context.InsuranceProducts
                .Include(ip => ip.Category)
                .Include(ip => ip.Type)
                .Include(ip => ip.Package)
                .Include(ip => ip.AuthorizedUser)
                .Include(ip => ip.Users) // Include if you want to retrieve related users
                .FirstOrDefaultAsync(ip => ip.ProductId == id);

            if (insuranceProduct == null)
            {
                return NotFound();
            }

            // Convert int values to string representations
            var insuranceProductDto = new InsuranceProductDto
            {
                Name = insuranceProduct.Name,
                CategoryName = insuranceProduct.Category.Name,
                TypeName = insuranceProduct.Type.Name,
                PackageName = insuranceProduct.Package.Name,
                AuthorizedUserName = insuranceProduct.AuthorizedUser.Name,
                Description = insuranceProduct.Description,
            };

            return insuranceProductDto;
        }

        // PUT: api/InsuranceProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, InsuranceProductDto dto)
        {
            var product = _context.InsuranceProducts.FirstOrDefault(x => x.ProductId == id);
            if (dto == null || product==null )
            {
                return NotFound();
            }

            product.Name = dto.Name;
            product.Description = dto.Description;

            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == dto.CategoryName);
                if (category != null)
                {
                    product.CategoryId = category.Id;
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
                    product.TypeId = type.Id;
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
                    product.PackageId = package.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.PackageName}' not found.");
                }
            }
            if (!string.IsNullOrEmpty(dto.AuthorizedUserName))
            {
                var authorizedUser = _context.AuthorizedUsers.FirstOrDefault(c => c.Name == dto.AuthorizedUserName);
                if (authorizedUser != null)
                {
                    product.AuthorizedUserId = authorizedUser.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.AuthorizedUserName}' not found.");
                }
            }
            await _context.SaveChangesAsync();

            return Ok("Object created");


        }
    

        // POST: api/InsuranceProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsuranceProduct>> PostInsuranceProduct(InsuranceProductDto dto)
        {
            if (_context.InsuranceProducts == null)
            {
                return Problem("Entity set 'AppDbContext.InsuranceProduct' is null.");
            }

            // Check if an insurance product with the same name already exists
            if (_context.InsuranceProducts.Any(ip => ip.Name == dto.Name))
            {
                return Conflict($"An insurance product with the name '{dto.Name}' already exists.");
            }

            var insuranceProduct = new InsuranceProduct
            {
                Name = dto.Name,
                Description = dto.Description
            };

            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                var category = _context.Categories.FirstOrDefault(c => c.Name == dto.CategoryName);
                if (category != null)
                {
                    insuranceProduct.CategoryId = category.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.CategoryName}' not found.");
                }
            }
            if (!string.IsNullOrEmpty(dto.TypeName))
            {
                var type= _context.Types.FirstOrDefault(c => c.Name == dto.TypeName);
                if (type != null)
                {
                    insuranceProduct.TypeId = type.Id;
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
                    insuranceProduct.PackageId = package.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.PackageName}' not found.");
                }
            }
            if (!string.IsNullOrEmpty(dto.AuthorizedUserName))
            {
                var authorizedUser = _context.AuthorizedUsers.FirstOrDefault(c => c.Name == dto.AuthorizedUserName);
                if (authorizedUser != null)
                {
                    insuranceProduct.AuthorizedUserId = authorizedUser.Id;
                }
                else
                {
                    return BadRequest($"Category with name '{dto.AuthorizedUserName}' not found.");
                }
            }
            _context.InsuranceProducts.Add(insuranceProduct);
            await _context.SaveChangesAsync();

            return Ok("Object created");
        }
        [HttpPost("Sell")]
        public async Task<ActionResult> SellInsuranceProductToUser(int userId, int insuranceProductId)
        {
            // Retrieve the User and InsuranceProduct entities
            var user = await _context.Users.FindAsync(userId);
            var insuranceProduct = await _context.InsuranceProducts.FindAsync(insuranceProductId);

            if (user == null || insuranceProduct == null)
            {
                return NotFound("User or InsuranceProduct not found.");
            }

            // Create the relationship by adding the InsuranceProduct to the User's collection
            user.InsuranceProducts.Add(insuranceProduct);

            // Alternatively, you can add the User to the InsuranceProduct's collection
             insuranceProduct.Users.Add(user);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Object created successfully");
        }

        // DELETE: api/InsuranceProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsuranceProduct(int id)
        {
            if (_context.InsuranceProducts == null)
            {
                return NotFound();
            }
            var insuranceProduct = await _context.InsuranceProducts.FindAsync(id);
            if (insuranceProduct == null)
            {
                return NotFound();
            }

            _context.InsuranceProducts.Remove(insuranceProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuranceProductExists(int id)
        {
            return (_context.InsuranceProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
