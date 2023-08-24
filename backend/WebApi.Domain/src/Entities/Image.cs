namespace WebApi.Domain.src.Entities
{
    public class Image : BaseEntity
    {
        public byte[] ImageData { get; set; }
        public string ProductId { get; set; }
    }
}