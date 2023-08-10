
using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepo<T>
    {
        Task<T> CreateOne(T entity);
        Task<T> GetOneById(string id);
        Task<bool> DeleteOneById(T entity);
        Task<T> UpdateOne(T entity,T updated);
        Task<IEnumerable<T>> GetAll(SearchQueryOptions options);
    }
}