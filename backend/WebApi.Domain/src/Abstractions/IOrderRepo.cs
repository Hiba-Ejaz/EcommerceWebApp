using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
  public interface IOrderRepo : IBaseRepo<Order>
  {
    Task<string> AddOrder(Order order);
    Task<string> UpdateOne(Order order);
    Task<Order?> GetOneByUserId(Guid userId);
    Task<IEnumerable<Order>> GetAllOrders();

  }
}