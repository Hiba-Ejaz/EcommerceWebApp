
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;

namespace WebApi.Controller.src.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class CartController:ControllerBase
    {
   
private readonly ICartService _cartService;
    
        public CartController(ICartService cartService)
        {
             _cartService = cartService;
        }
        [HttpPost("add-to-cart")]
        public async Task<ActionResult<string>> AddToCart([FromBody] AddToCartDto addToCartDto)
        {

             var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
           
            if (loggedInUserIdClaim == null)
             {
        return Unauthorized(); // User not authenticated
                 }

               var userId = loggedInUserIdClaim.Value;

             if (Guid.TryParse(userId, out Guid userIdGuid))
             {
              // return "hello";
      return await _cartService.AddToCart(userIdGuid, addToCartDto);  
    }
    else
    {
      
        return BadRequest("Invalid user ID format"); 
    }
           
      

    
    }
    }}