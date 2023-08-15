

namespace WebApi.Domain.src.Entities
{
    public class Image:BaseEntity
    {

        public byte[] ImageData { get; set; }  // Image binary data

        public string ProductId { get; set; } 
      
    }
}