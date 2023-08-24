using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IAuthRepo
    {
        public string CreateToken(User user);
    }
}