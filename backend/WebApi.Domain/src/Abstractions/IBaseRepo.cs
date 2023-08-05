
using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepo<T>
    {
        T CreateOne(T entity);
        T GetOneById(string id);
        bool DeleteOneById(T entity);
        T UpdateOne(T entity,T updated);
        IEnumerable<T> GetAll(SearchQueryOptions options);
    }
}