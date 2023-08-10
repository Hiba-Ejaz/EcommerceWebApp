using Microsoft.AspNetCore.Mvc; 
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
    [Authorize]
    public class UserController:CrudController<User, UserCreateDto,UserReadDto,UserUpdateDto>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService):base(userService)
        {
             _userService=userService;
        }
          [HttpPatch("{id}/updatepassword")] //
          public async Task<ActionResult<UserReadDto>> UpdatePassword([FromRoute] string id, [FromBody] string password){
             return await _userService.UpdatePassword(id,password);
          }
    }
}