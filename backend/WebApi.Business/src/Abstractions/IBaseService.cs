
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IBaseService<T,TCreateDto, TReadDto, TUpdateDto>
    {
        Task<TReadDto> GetOneById(string id);
        Task<bool> DeleteOneById(string id);
        Task<TReadDto> UpdateOneById(string id,TUpdateDto entityToUpdate); //cannot update password
        Task<IEnumerable<TReadDto>> GetAll(SearchQueryOptions options);
        Task<TReadDto> CreateOne(TCreateDto dto);
    }
}