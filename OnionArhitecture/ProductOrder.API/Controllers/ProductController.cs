using Microsoft.AspNetCore.Mvc;
using ProductOrder.Application.Services;
using ProductOrder.Domain.Entities;

namespace ProductOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            var newProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<Product>>> SearchProducts([FromQuery] string name)
        {
            var products = await _productService.GetProductsByNameAsync(name);
            return Ok(products);
        }

        [HttpGet("price-range")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var products = await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpGet("top-selling")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetTopSellingProducts([FromQuery] int topN)
        {
            var products = await _productService.GetTopSellingProductsAsync(topN);
            return Ok(products);
        }

        [HttpPut("update-stock")]
        public async Task<IActionResult> UpdateProductStock([FromQuery] Guid productId, [FromQuery] int stock)
        {
            await _productService.UpdateProductStockAsync(productId, stock);
            return NoContent();
        }
    }
}