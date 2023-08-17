

namespace WebApi.Domain.src.Entities
{
   public class OrderItem : BaseEntityWithoutId
    {
        
        public Order Order{ get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}