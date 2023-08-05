
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IBaseService<T,TDto>
    {
        TDto GetOneById(string id);
        bool DeleteOneById(string id);
        TDto UpdateOneById(string id,TDto entityToUpdate); //cannot update password
        IEnumerable<TDto> GetAll(SearchQueryOptions options);

    }
}