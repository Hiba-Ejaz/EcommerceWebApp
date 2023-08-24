
namespace WebApi.Domain.src.Entities
{
    public class Cart : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public Decimal TotalAmount { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}