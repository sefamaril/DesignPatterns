using ProductOrder.Domain.Entities;
using ProductOrder.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductOrder.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _productRepository.GetAsync(predicate);
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddAsync(product);
        }
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
            }
        }
        public async Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name)
        {
            return await _productRepository.GetProductsByNameAsync(name);
        }
        public async Task<IReadOnlyList<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _productRepository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
        }
        public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _productRepository.GetProductsByCategoryAsync(category);
        }
        public async Task<IReadOnlyList<Product>> GetTopSellingProductsAsync(int topN)
        {
            return await _productRepository.GetTopSellingProductsAsync(topN);
        }
        public async Task UpdateProductStockAsync(Guid productId, int stock)
        {
            await _productRepository.UpdateProductStockAsync(productId, stock);
        }
        //Other additional methods...
    }
}