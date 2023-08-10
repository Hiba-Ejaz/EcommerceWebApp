
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderCreateDto
    {
        public UserReadDto User{ get; set; }
        public OrderStatus Status { get; set; }  
         public Decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
      
    }
     public class OrderReadDto
    {
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }  
        public Decimal TotalAmount { get; set; }
        public List<OrderItemsDto> OrderItems { get; set; }
    }
     public class OrderUpdateDto
    {
        public OrderStatus Status { get; set; }  
        public List<OrderItem> OrderItems { get; set; }
    }
}