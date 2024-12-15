using Microsoft.AspNetCore.Mvc;
using ProductOrder.Application.Services;
using ProductOrder.Domain.Entities;

namespace ProductOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
        {
            var newOrder = await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersByProductId(Guid productId)
        {
            var orders = await _orderService.GetOrdersByProductIdAsync(productId);
            return Ok(orders);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
            return Ok(orders);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersByCustomer(Guid customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("total-sales")]
        public async Task<ActionResult<decimal>> GetTotalSales([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var totalSales = await _orderService.GetTotalSalesAsync(startDate, endDate);
            return Ok(totalSales);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus([FromQuery] Guid orderId, [FromQuery] string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return NoContent();
        }
    }
}