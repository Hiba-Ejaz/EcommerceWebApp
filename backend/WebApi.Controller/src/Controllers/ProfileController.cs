using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public ProfileController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<UserReadDto>> GetUserProfile()
        {
            try
            {
                var jwtKey = _configuration["Security:JwtKey"];
                var jwtIssuer = _configuration["Security:Issuer"];
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (userIdClaim == null || roleClaim == null)
                {
                    return StatusCode(401, "Unauthorized");
                }
                Guid userId = Guid.Parse(userIdClaim.Value);
                string role = roleClaim.Value;
                UserReadDto userProfile = await _userService.GetOneById(userId);
                if (userProfile == null)
                {
                    return NotFound();
                }
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the user profile.");
            }
        }
    }
}
