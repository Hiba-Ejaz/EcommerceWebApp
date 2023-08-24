
namespace WebApi.Business.src.Dtos
{
    public class AddToCartDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartReadDto
    {
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalAmount { get; set; }

    }
}

