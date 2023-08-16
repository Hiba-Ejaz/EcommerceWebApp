
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IBaseService<T,TCreateDto, TReadDto, TUpdateDto>
    {
        Task<TReadDto> GetOneById(Guid id);
        Task<bool> DeleteOneById(Guid id);
        // Task<TReadDto> UpdateOneById(Guid id,TUpdateDto entityToUpdate); //cannot update password
        Task<IEnumerable<TReadDto>> GetAll(SearchQueryOptions options);
        Task<TReadDto> CreateOne(TCreateDto dto);
    }
}