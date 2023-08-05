

namespace WebApi.Domain.src.Entities
{
    public class Product: BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<Image> Images { get; set; }
        public int Quantity { get; set; } 
    }
}