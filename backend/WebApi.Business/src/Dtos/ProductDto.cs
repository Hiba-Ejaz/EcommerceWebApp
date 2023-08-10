

using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Dtos
{
    public class ProductCreateDto
    
    {

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } // Assuming this is used to associate the product with a category
        public int Quantity { get; set; }
        public List<Image> Images { get; set; }
    }
    public class ProductReadDto
    {
        
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set;}
         public List<Image> Images { get; set; }
    }
      public class ProductUpdateDto
    {
        
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set;}  
        public int CategoryId { get; set; }
         public List<Image> Images { get; set; }
        public int Quantity { get; set; }
    }

}