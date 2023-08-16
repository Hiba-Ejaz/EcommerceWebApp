
using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepo<T>
    {
        Task<T> CreateOne(T entity);
        Task<T?> GetOneById(Guid id);
        Task<bool> DeleteOneById(T entity);
      // Task<T> UpdateOne(T updated);
        Task<IEnumerable<T>> GetAll(SearchQueryOptions options);
    }
}