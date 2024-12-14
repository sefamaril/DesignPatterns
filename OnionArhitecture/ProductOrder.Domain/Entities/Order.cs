using ProductOrder.Domain.Common;

namespace ProductOrder.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total => Quantity * Product.Price; // Business logic example
        public Product Product { get; set; }
    }
}