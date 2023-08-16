

namespace WebApi.Domain.src.Entities
{
   public class OrderItem : BaseEntityWithoutId
    {
        // public Guid OrderId { get; set; }
        // public Guid ProductId { get; set; }
        public Order Order{ get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}