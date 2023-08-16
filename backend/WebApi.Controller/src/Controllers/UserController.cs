using Microsoft.AspNetCore.Mvc; 
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


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

// [HttpPatch("updatepassword")]
// public ActionResult<bool> UpdatePassword([FromBody] string password)
// {
//     var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

//     if (loggedInUserIdClaim == null)
//     {
//         return Ok(false); // Claim not present
//     }

//     // Claim is present
//     return Ok(true);
// }



    [HttpPatch("updatepassword")]
        public async Task<ActionResult<string>> UpdatePassword([FromBody] string password)
        {
           var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
           
            if (loggedInUserIdClaim == null)
    {
        return Unauthorized(); // User not authenticated
    }

               var userId = loggedInUserIdClaim.Value;

             if (Guid.TryParse(userId, out Guid userIdGuid))
    {
       return await _userService.UpdatePassword(userIdGuid, password);

      // return "Password updated successfully";
    }
    else
    {
        return BadRequest("Invalid user ID format"); 
    }
        }

          // [HttpPatch("{id:Guid}/updatepassword")] //
          // public async Task<ActionResult<UserReadDto>> UpdatePassword([FromRoute] Guid id, [FromBody] string password){
          //    return await _userService.UpdatePassword(id,password);
          // }

          // [HttpPost("admin")] 
          // [Authorize(Roles = "admin")]
          //  public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto user){
          //    return await _userService.CreateAdmin(user);
          // }

        [HttpGet] 
        [Authorize(Roles = "admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _userService.GetAll(options));
        }

          
    }
}