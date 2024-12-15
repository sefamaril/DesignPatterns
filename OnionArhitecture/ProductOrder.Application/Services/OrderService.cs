using ProductOrder.Domain.Entities;
using ProductOrder.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ProductOrder.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }
        public async Task<IReadOnlyList<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _orderRepository.GetAsync(predicate);
        }
        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }
        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }
        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
            }
        }
        public async Task<IReadOnlyList<Order>> GetOrdersByProductIdAsync(Guid productId)
        {
            return await _orderRepository.GetOrdersByProductIdAsync(productId);
        }
        public async Task<IReadOnlyList<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
        }
        public async Task<IReadOnlyList<Order>> GetOrdersByCustomerAsync(Guid customerId)
        {
            return await _orderRepository.GetOrdersByCustomerAsync(customerId);
        }
        public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.GetTotalSalesAsync(startDate, endDate);
        }
        public async Task UpdateOrderStatusAsync(Guid orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
        }
    }
}