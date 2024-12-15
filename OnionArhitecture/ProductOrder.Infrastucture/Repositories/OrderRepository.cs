using Microsoft.EntityFrameworkCore;
using ProductOrder.Domain.Entities;
using ProductOrder.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductOrder.Infrastucture.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnionDbContext _context;
        public OrderRepository(OnionDbContext onionDbContext)
        {
            _context = onionDbContext;
        }
        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders.Where(predicate).ToListAsync();
        }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<Order> AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order entity)
        {
            var order = await GetByIdAsync(entity.Id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IReadOnlyList<Order>> GetOrdersByProductIdAsync(Guid productId)
        {
            return await _context.Orders.Where(o => o.ProductId == productId).ToListAsync();
        }
        public async Task<IReadOnlyList<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
        }
        public Task<IReadOnlyList<Order>> GetOrdersByCustomerAsync(Guid customerId)
        {
            // Assuming there is a CustomerId property in the Order entity
            // return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
            throw new NotImplementedException();
        }
        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).SumAsync(o => o.Total);
        }
        public async Task UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                // order.Status = status; // Assuming there is a Status property in the Order entity
                await _context.SaveChangesAsync();
            }
        }
    }
}