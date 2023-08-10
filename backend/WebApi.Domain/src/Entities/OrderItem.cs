

namespace WebApi.Domain.src.Entities
{
   public class OrderItem : BaseEntity
    {
        public Order OrderId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}