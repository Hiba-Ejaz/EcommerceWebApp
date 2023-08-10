using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
[Authorize]
    public class OrderController:CrudController<Order, OrderCreateDto,OrderReadDto,OrderUpdateDto>
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService):base(orderService)
        {
             _orderService=orderService;
        }
    
        
    }
}