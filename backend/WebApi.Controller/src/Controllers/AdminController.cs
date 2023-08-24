using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    public class AdminController
    {
        private readonly IOrderService _orderService;
        public AdminController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("orders")]
        public async Task<IEnumerable<OrderWithDetailsReadDto>> GetOrderItems()
        {
            return await _orderService.GetAllOrders();
        }
    }
}