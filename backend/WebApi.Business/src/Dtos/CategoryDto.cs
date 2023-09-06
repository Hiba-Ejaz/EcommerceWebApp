namespace WebApi.Business.src.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }
    public class CategoryReadDto
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
    }
}