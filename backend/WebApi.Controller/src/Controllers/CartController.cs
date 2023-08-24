using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
     [Authorize(Roles="Customer")]
     [ApiController]
     [Route("api/v1/[controller]s")]
     public class CartController : ControllerBase
     {
          private readonly ICartService _cartService;
          public CartController(ICartService cartService)
          {
               _cartService = cartService;
          }
         
          [HttpDelete]
          public async Task<ActionResult<string>> Delete()
          {

               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return BadRequest("User claim is null");
               }
               var userId = loggedInUserIdClaim.Value;
               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    return Ok(await _cartService.DeleteCart(userIdGuid));
               }
               return BadRequest("Invalid user ID format");
          }
          [Authorize(Roles = "Customer")]
          [HttpGet("items")]
          public async Task<ActionResult<IEnumerable<CartReadDto>>> GetCartItems()
          {
               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return BadRequest("User claim is null");
               }
               var userId = loggedInUserIdClaim.Value;
               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    // return "failed in parsing";
                    return Ok(await _cartService.GetCartItems(userIdGuid));
               }
               else
               {
                    return BadRequest("Invalid user ID format");
               }
          }
          [HttpPatch]
          //[Authorize(Roles = "Customer")]
          public async Task<ActionResult<string>> AddToCart([FromBody] AddToCartDto addToCartDto)
          {
               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return "user claim is null"; // User not authenticated
               }
               var userId = loggedInUserIdClaim.Value;
               // var userId=await GetUserFromClaim();
               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    return await _cartService.AddToCart(userIdGuid, addToCartDto);
               }
               else
               {
                    return BadRequest("Invalid user ID format");
               }
          }

          [HttpDelete("items/{productId}")]
          // [Authorize(Roles = "Customer")]
          public async Task<ActionResult<string>> RemoveFromCart([FromRoute] Guid productId)
          {
               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return "cliam is null";
                    return BadRequest("User claim is null"); // User not authenticated
               }
               var userId = loggedInUserIdClaim.Value;

               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    return await _cartService.RemoveFromCart(userIdGuid, productId);
               }
               else
               {
                    return BadRequest("Invalid user ID format");
               }
          }
     }
}