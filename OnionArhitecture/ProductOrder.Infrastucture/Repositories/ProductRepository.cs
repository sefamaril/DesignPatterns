using Microsoft.EntityFrameworkCore;
using ProductOrder.Domain.Entities;
using ProductOrder.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductOrder.Infrastucture.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnionDbContext _context;
        public ProductRepository(OnionDbContext onionDbContext)
        {
            _context = onionDbContext;
        }
        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<IReadOnlyList<Product>> GetAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Where(predicate).ToListAsync();
        }
        public async Task<Product> GetByIdAsync(Guid id)
        {
            //var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            //var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            //var product = await _context.Products
            //.Include(p => p.Category) // As an example we include the Category relationship
            //.FirstOrDefaultAsync(p => p.Id == id);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                // Handle the case where the product is not found
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }
        public async Task<Product> AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Product entity)
        {
            var product = await GetByIdAsync(entity.Id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }
        public async Task<IReadOnlyList<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }
        public Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(string category)
        {
            //Assuming there is a Category property in the Product entity
            //return await _context.Products.Where(p => p.Category == category).ToListAsync();
            throw new NotImplementedException();
        }
        public Task<IReadOnlyList<Product>> GetTopSellingProductsAsync(int topN)
        {
            //Assuming there is a SalesCount property in the Product entity
            //return await _context.Products.OrderByDescending(p => p.SalesCount).Take(topN).ToListAsync();
            throw new NotImplementedException();
        }
        public async Task UpdateProductStockAsync(Guid productId, int stock)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                //Assuming there is a Stock property in the Product entity
                //product.Stock = stock; 
                await _context.SaveChangesAsync();
            }
        }
    }
}