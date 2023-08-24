using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controller.src.Controllers
{
     // [Authorize]
     [ApiController]
     [Route("api/v1/[controller]")]
     public class OrderController : CrudController<Order, OrderCreateDto, OrderReadDto, OrderUpdateDto>
     {
          private readonly IOrderService _orderService;
          public OrderController(IOrderService orderService) : base(orderService)
          {
               _orderService = orderService;
          }
          [HttpPatch]
          //[Authorize(Roles = "Customer")]
          public async Task<ActionResult<string>> AddOrder()
          {
               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return "user claim is null";
               }
               var userId = loggedInUserIdClaim.Value;
               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    return await _orderService.AddOrder(userIdGuid);
               }
               else
               {
                    return BadRequest("Invalid user ID format");
               }
          }
          [HttpGet("my-orders")]
          public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrderItems()
          {
               var loggedInUserIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
               if (loggedInUserIdClaim == null)
               {
                    return BadRequest("User claim is null");
               }
               var userId = loggedInUserIdClaim.Value;
               if (Guid.TryParse(userId, out Guid userIdGuid))
               {
                    return Ok(await _orderService.GetOrder(userIdGuid));
               }
               else
               {
                    return BadRequest("Invalid user ID format");
               }
          }
     }
}