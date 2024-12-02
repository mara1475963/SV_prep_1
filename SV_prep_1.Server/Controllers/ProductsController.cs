using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SV_prep_1.Server.Data;
using SV_prep_1.Server.Models;

namespace SV_prep_1.Server.Controllers
{
    [Route("api/Stores/{storeId}/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Stores/{storeId}/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int storeId)
        {
            var products = await _context.Products
                .Where(p => p.StoreId == storeId) // Filter products by StoreId
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound($"No products found for store with ID {storeId}.");
            }

            return Ok(products);
        }

        // POST: api/Stores/{storeId}/products
        [HttpPost]
        public async Task<ActionResult<Product>> Post(int storeId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null.");
            }

            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return NotFound($"Store with ID {storeId} not found.");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                StoreId = storeId // Set StoreId
            };

            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { storeId = storeId, id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Stores/{storeId}/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int storeId, int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null.");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.StoreId == storeId);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found in store with ID {storeId}.");
            }

            // Update product properties
            product.Name = productDto.Name;
            product.Price = productDto.Price;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Stores/{storeId}/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int storeId, int id)
        {
            Console.WriteLine(storeId.ToString(), id);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.StoreId == storeId);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found in store with ID {storeId}.");
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
