using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IOrderItemRepo : IBaseRepo<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItems(Guid userId);
        Task<string> UpdateOne(OrderItem orderItem);
    }
}