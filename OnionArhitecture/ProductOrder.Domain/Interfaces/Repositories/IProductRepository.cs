using ProductOrder.Domain.Entities;

namespace ProductOrder.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name);
        Task<IReadOnlyList<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(string category);
        Task<IReadOnlyList<Product>> GetTopSellingProductsAsync(int topN);
        Task UpdateProductStockAsync(Guid productId, int stock);

    }
}