
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }  
        public List<OrderItemDto> OrderItems { get; set; }
    }
}