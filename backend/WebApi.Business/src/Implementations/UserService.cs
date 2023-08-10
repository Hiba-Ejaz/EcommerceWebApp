
using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class UserService: BaseService<User,UserCreateDto,UserReadDto,UserUpdateDto>, IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo,IMapper mapper):base(userRepo,mapper)
        {
         _userRepo=userRepo;  
        }
        public async Task<UserReadDto> UpdatePassword(string id, string password)
        {
           var foundItem=await _userRepo.GetOneById(id);
           if(foundItem==null){
            throw new Exception("item not found");
           }
           var updatedEntity=await _userRepo.UpdatePassword(foundItem,password);
           return _mapper.Map<UserReadDto>(updatedEntity);
        }

       
    }
}