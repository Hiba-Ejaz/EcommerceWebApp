using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        Task<Product?> FindProductForUpdate(Guid id);
        Task<Product> UpdateOne(Guid id, Product updated);
        Task<string> FindOneById(Guid id);
    }
}