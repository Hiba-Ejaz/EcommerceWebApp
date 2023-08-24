namespace WebApi.Domain.src.Entities
{
    public class CartItem : BaseEntityWithoutId
    {
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}