

using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IUserRepo:IBaseRepo<User>
    {
        User CreateAdmin(User user);
        User UpdatePassword(User user, string password);
    }
}