using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _genericRepo;

        public ProductsController(IGenericRepository<Product> genericRepo)
        {
            _genericRepo = genericRepo;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);

            var products = await _genericRepo.ListAsync(spec);

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _genericRepo.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _genericRepo.Add(product);
            await _genericRepo.SaveAllAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !_genericRepo.Exists(id))
                return BadRequest();

            _genericRepo.Update(product);
            await _genericRepo.SaveAllAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _genericRepo.GetByIdAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }
            _genericRepo.Remove(product);

            if ( await _genericRepo.SaveAllAsync() == true)
            {
                return NoContent();
            }
            return BadRequest("Problem deleting product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            //var brands = await _genericRepo.GetBrandsAsync();
            return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            //var types = await _genericRepo.GetTypesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByFilter(string? brand, string? type, string? sort)
        {
            ProductSpecification spec = new ProductSpecification(brand, type, sort);
            var product = await _genericRepo.GetEntityWithSpec(spec);

            if (product == null)
                return NotFound("No products found matching the specified filters.");

            return Ok(product);
        }
    }
}
    