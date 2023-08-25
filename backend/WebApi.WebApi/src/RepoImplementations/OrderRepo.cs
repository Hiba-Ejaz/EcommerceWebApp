using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DatabaseContext _dbcontext;
        public OrderRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _orders = dbContext.Orders;
            _dbcontext = dbContext;
        }
        public async Task<string> AddOrder(Order order)
        {
            await _orders.AddAsync(order);
            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error updating Order : {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
                }
                return errorMessage;
            }
            return "order addded successfully";
        }
       
        public async Task<string> UpdateOne(Order order)
        {
            _orders.Update(order);
            _dbcontext.Entry(order).Collection(o => o.OrderItems).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return "order table updated";
        }
      
        public async Task<Order?> GetOneByUserId(Guid userId)
        {
            var order = await _orders
             .Include(o => o.OrderItems)
              .ThenInclude(ci => ci.Product)
              .SingleOrDefaultAsync(o => o.UserId == userId
           && o.Status == OrderStatus.Processing);
            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _orders
           .Include(o => o.OrderItems)
           .ThenInclude(ci => ci.Product)
           .Include(ci => ci.User)
           .ToListAsync();
            return orders;
        }
    }
}