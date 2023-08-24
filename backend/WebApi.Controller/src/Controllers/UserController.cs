using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controller.src.Controllers
{
    // [Authorize]
    public class UserController : CrudController<User, UserCreateDto, UserReadDto, UserUpdateDto>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
        [HttpPatch("updatepassword")]
        [Authorize]
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

        [HttpGet]
         [Authorize(Roles = "admin")]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
            return Ok(await _userService.GetAll(options));
        }
        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto created)
        {
            var createdObject = await _userService.CreateOne(created);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

         [HttpGet("{id:Guid}")]
         [Authorize]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.GetOneById(id));
        }

        [HttpDelete("{id:Guid}")]
         [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<UserReadDto>> DeleteOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.DeleteOneById(id));
        }

        [HttpPost("admin")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto dto)
        {
            return await _userService.CreateAdmin(dto);
        }
    }
}