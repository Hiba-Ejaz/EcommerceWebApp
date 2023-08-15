
namespace WebApi.Domain.src.Entities
{
   public enum OrderStatus
    {
        Processing,
        Shipped,
        Delivered,
       
    }

    public class Order : BaseEntity
    {
        
        public User User { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; } 
         public Decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}