namespace WebApi.Business.src.Dtos
{
  public class ProductCreateDto

  {
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    // public Guid CategoryId { get; set; } 
    public int Quantity { get; set; }
    public string[] Images { get; set; }
    //public List<Image> Images { get; set; }
    //public List<Guid> ImageIds { get; set; }
  }
  public class ProductReadDto
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
      public string[] Images { get; set; }
    //   public List<Image> Images { get; set; }
    //   public List<Guid> ImageIds { get; set; }
  }
  public class ProductUpdateDto
  {

    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string[] Images { get; set; }
    // public Guid CategoryId { get; set; }
    // public List<Image> Images { get; set; }
    // public List<Guid> ImageIds { get; set; }
    public int Quantity { get; set; }
  }

}