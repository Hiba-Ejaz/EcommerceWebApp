using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.src.Abstractions
{
    public interface IBaseRepo
    {
        T CreateOne(T entity);
        T GetOneById(Guid id);
        bool DeleteOneById(Guid id);
        T UpdateOne(T entity);
        IEnumerable<T> GetAll(SearchQueryOptions options);
    }
}