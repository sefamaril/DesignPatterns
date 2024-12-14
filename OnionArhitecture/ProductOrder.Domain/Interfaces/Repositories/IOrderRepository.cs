using ProductOrder.Domain.Entities;

namespace ProductOrder.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IReadOnlyList<Order>> GetOrdersByProductIdAsync(Guid productId);
        Task<IReadOnlyList<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IReadOnlyList<Order>> GetOrdersByCustomerAsync(Guid customerId);
        Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
        Task UpdateOrderStatusAsync(Guid orderId, string status);
    }
}