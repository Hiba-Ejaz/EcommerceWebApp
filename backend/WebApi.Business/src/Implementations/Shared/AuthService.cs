


using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations.Shared
{
    public class AuthService : IAuthService
    {
      private readonly IUserRepo _userRepo;
      private readonly IAuthRepo _authRepo;
      public AuthService(IUserRepo userRepo,IAuthRepo authRepo)
      {
        _userRepo=userRepo;
        _authRepo=authRepo;
      }
        public async Task<string> VerifyCredentials(UserCredentialsDto userCredentials)
        {
            var foundUser=await _userRepo.FindUserByEmail(userCredentials.Email) ?? throw new Exception("user not found");
            var isAutheticated =  PasswordService.VerifyPassword(userCredentials.Password,foundUser.Password,foundUser.Salt);
            if(!isAutheticated){
                throw new Exception("credentials donot match");
            }
            return GenerateToken(foundUser);
            }
        
        private string GenerateToken(User user)
        {
           return  _authRepo.CreateToken(user);
        }
    }
}