
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
  
     public class OrderRepo : BaseRepo<Cart>, IOrderRepo
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
            
           try{
            await _dbcontext.SaveChangesAsync();
           }  catch (Exception ex)
    {
        string errorMessage = $"Error updating Order ya yeh: {ex.Message}";
        if (ex.InnerException != null)
        {
            errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
        }
        return errorMessage;
    }
            
            return "order addded successfully";
        }

        public Task<Order> CreateOne(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneById(Order entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Order>> IBaseRepo<Order>.GetAll(SearchQueryOptions options)
        {
            throw new NotImplementedException();
        }

        Task<Order?> IBaseRepo<Order>.GetOneById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}