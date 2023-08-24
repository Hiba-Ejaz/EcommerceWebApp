
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IBaseService<T,TCreateDto, TReadDto, TUpdateDto>
    {
        Task<TReadDto> GetOneById(Guid id);
        Task<bool> DeleteOneById(Guid id);
        Task<IEnumerable<TReadDto>> GetAll(SearchQueryOptions options);
        Task<TReadDto> CreateOne(TCreateDto dto);
    }
}