

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
   //[Authorize]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController( IAuthService authService)
        {
             _authService=authService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> VerifyCreditials([FromBody] UserCredentialsDto credentials){
          return Ok(await _authService.VerifyCredentials(credentials));
        }
    }
}