using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            //var products = await _productRepo.GetProductsAsync();
            var products = await _productRepo.GetProductsByFilter(brand, type, sort);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _productRepo.AddProduct(product);
            await _productRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !_productRepo.ProductExists(id))
                return BadRequest();

            _productRepo.UpdateProduct(product);
            await _productRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!_productRepo.ProductExists(id))
                return NotFound();

            _productRepo.DeleteProduct(id);
            await _productRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            var brands = await _productRepo.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetTypes()
        {
            var types = await _productRepo.GetTypesAsync();
            return Ok(types);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProductsByFilter([FromQuery] string? brand, [FromQuery] string? type)
        //{
        //    var products = await _productRepo.GetProducts(brand, type);

        //    if (products == null || !products.Any())
        //        return NotFound("No products found matching the specified filters.");

        //    return Ok(products);
        //}
    }
}
    