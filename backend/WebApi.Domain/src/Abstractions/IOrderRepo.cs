

using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IOrderRepo:IBaseRepo<Order>{
         Task<Order> GetOrCreateCartForUser(Guid userId);
        Task<string> AddOrder(Order order);
        Task<Order> GetProcessingOrderByUserId(Guid userId);
    }
}