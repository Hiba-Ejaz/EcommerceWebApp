
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class OrderCreateDto
    {
        public UserReadDto User { get; set; }
        public OrderStatus Status { get; set; }
        public Decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
    public class OrderReadDto
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalAmount { get; set; }
    }
    public class OrderUpdateDto
    {
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}

public class OrderWithDetailsReadDto
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public List<OrderProductsReadDto> orderItems { get; set; }
    public Decimal TotalAmount { get; set; }
}
public class OrderProductsReadDto
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}