using ProductOrder.Domain.Common;

namespace ProductOrder.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}