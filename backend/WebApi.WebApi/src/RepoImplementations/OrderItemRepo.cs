using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class OrderItemRepo : BaseRepo<OrderItem>, IOrderItemRepo
    {
        private readonly DbSet<OrderItem> _orderItem;
        private readonly DatabaseContext _dbcontext;
        public OrderItemRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _orderItem = dbContext.OrderItems;
            _dbcontext = dbContext;
        }
        public async Task<IEnumerable<OrderItem>> GetOrderItems(Guid userId)
        {
            var orderItems = await _orderItem
           .Where(oi => oi.Order.UserId == userId)
           .Include(oi => oi.Product)
           .Include(oi => oi.Order)
           .ToListAsync();
            return orderItems;
        }
        public async Task<string> UpdateOne(OrderItem orderItem)
        {
            _orderItem.Update(orderItem);
            await _dbcontext.SaveChangesAsync();
            return "order item updated";
        }
    }


}