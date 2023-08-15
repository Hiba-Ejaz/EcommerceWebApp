using Microsoft.AspNetCore.Mvc; 
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
    //[Authorize]
    public class UserController:CrudController<User, UserCreateDto,UserReadDto,UserUpdateDto>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService):base(userService)
        {
             _userService=userService;
        }
          [HttpPatch("{id:Guid}/updatepassword")] //
          public async Task<ActionResult<UserReadDto>> UpdatePassword([FromRoute] Guid id, [FromBody] string password){
             return await _userService.UpdatePassword(id,password);
          }

          [HttpPost("admin")] 
          [Authorize(Roles = "admin")]
           public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto user){
             return await _userService.CreateAdmin(user);
          }

        [HttpGet] 
        [Authorize(Roles = "admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _userService.GetAll(options));
        }

          
    }
}