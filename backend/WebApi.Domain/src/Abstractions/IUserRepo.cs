using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IUserRepo : IBaseRepo<User>
    {
        Task<User> CreateAdmin(User user);
        Task<string> UpdatePassword(User user);
        Task<User?> FindUserByEmail(string email);
    }
}