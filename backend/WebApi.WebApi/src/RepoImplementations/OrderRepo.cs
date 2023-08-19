
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
        public async Task<Order> AddOrder(Order order)
        {
            await _orders.AddAsync(order);
                await _dbcontext.SaveChangesAsync();
                return order;
        }
          //  try
          //  {
            //   await _orders.AddAsync(order);
             //   await _dbcontext.SaveChangesAsync();
             //   return order;
          //  }
            // catch (Exception ex)
            // {
            //     string errorMessage = $"Error adding order: {ex.Message}";
            //     if (ex.InnerException != null)
            //     {
            //         errorMessage += $" ya yeh wali (Inner Exception: {ex.InnerException.Message})";
            //     }
            //     return errorMessage;
            // }
      //  }
public async Task<string> UpdateOrder(Order order)
{
    try
    {
        _dbcontext.Entry(order).State = EntityState.Modified;
        _orders.Update(order);
        _dbcontext.Entry(order).Collection(o => o.OrderItems).IsModified = true;
        await _dbcontext.SaveChangesAsync();
        return "Order updated successfully";
    }
    catch (Exception ex)
    {
        string errorMessage = $"Error updating order ya yeh: {ex.Message}";
        if (ex.InnerException != null)
        {
            errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
        }
        return errorMessage;
    }
}



        public async Task<Order?> GetProcessingOrderByUserId(Guid userId)
        {
            var order = await _orders
               .Include(o => o.OrderItems)
                .SingleOrDefaultAsync(o => o.UserId == userId);
                // && o.Status == OrderStatus.Processing);
               return order;
        }
  

        public Task<bool> DeleteOneById(Order entity)
        {
            throw new NotImplementedException();
        } 
        }
    }
