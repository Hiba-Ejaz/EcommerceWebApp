using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Business.src.Implementations.Shared;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class UserService : BaseService<User, UserCreateDto, UserReadDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _userRepo = userRepo;
        }
        public async Task<string> UpdatePassword(Guid id, string password)
        {
            var foundItem = await _userRepo.GetOneById(id) ?? throw CustomException.NotFoundException();
            PasswordService.HashPassword(password, out string hashedpassword, out Byte[] salt);
            foundItem.Password = hashedpassword;
            foundItem.Salt = salt;
            return await _userRepo.UpdatePassword(foundItem);
        }
        public async Task<UserReadDto> CreateAdmin(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out string hashedpassword, out Byte[] salt);
            entity.Password = hashedpassword;
            entity.Salt = salt;
            var createdEntity = await _userRepo.CreateAdmin(entity);
            return _mapper.Map<UserReadDto>(createdEntity);
        }
        public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out string hashedpassword, out Byte[] salt);
            entity.Password = hashedpassword;
            entity.Salt = salt;
            var createdEntity = await _userRepo.CreateOne(entity);
            return _mapper.Map<UserReadDto>(createdEntity);
        }
    }
}