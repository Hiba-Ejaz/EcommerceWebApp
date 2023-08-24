using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IUserService : IBaseService<User, UserCreateDto, UserReadDto, UserUpdateDto>
    {
        Task<UserReadDto> CreateAdmin(UserCreateDto user);
        Task<string> UpdatePassword(Guid id, string password);

    }
}