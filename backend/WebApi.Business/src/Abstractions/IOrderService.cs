using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IOrderService:IBaseService<Order,OrderCreateDto,OrderReadDto,OrderUpdateDto>
    {
        public Task<string> AddOrder(Guid userIdGuid);
        public Task<IEnumerable<OrderReadDto>> GetOrder(Guid userIdGuid);
        Task<IEnumerable<OrderWithDetailsReadDto>> GetAllOrders();
        // Task<string> GetAllOrders();
    }
}