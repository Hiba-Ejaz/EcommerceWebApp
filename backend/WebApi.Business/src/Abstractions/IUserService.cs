

using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IUserService:IBaseService<User,UserDto>
    {
        UserDto UpdatePassword(string id, string password); //specific to User
        //UserDto GetProfile(string id); //we will add this only in controller bcz getOneById and this has the same logic.
    }
}