

namespace WebApi.Domain.src.Entities
{
    public class Image:BaseEntity
    {
        public string Link{ get; set; }
        public int ProductId { get; set; } // The foreign key to relate the image to a product
    }
}