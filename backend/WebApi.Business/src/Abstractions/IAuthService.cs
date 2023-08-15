
using WebApi.Business.src.Dtos;

namespace WebApi.Business.src.Abstractions
{
    public interface IAuthService
    {
        public Task<string> VerifyCredentials(UserCredentialsDto userCredentials);
    }
}