
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class OrderRepo : BaseRepo<Order>,IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DatabaseContext _dbcontext;

        public OrderRepo(DatabaseContext dbContext)  : base(dbContext)
        {
             _orders=dbContext.Orders;
            _dbcontext=dbContext;
        }

        public async Task<string> AddOrder(Order order)
        {
          try
    {

        _orders.Add(order);
        await _dbcontext.SaveChangesAsync();
        return "Order added successfully";
    }
    catch (Exception ex)
    {
     
     
          string errorMessage = $"Error adding order: {ex.Message}";
        if (ex.InnerException != null)
        {
            errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
        }

        return errorMessage;
    }
        }

        public async Task<Order?> GetProcessingOrderByUserId(Guid userId)
        {
            var order= await _orders
                .Include(o => o.OrderItems)
                .SingleOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Processing);
       
        return order;
    
        }

        public Task<bool> DeleteOneById(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrCreateCartForUser(Guid userId)
        {

            var existingOrder = await _orders
        .Include(o => o.OrderItems)
        .SingleOrDefaultAsync(o => o.UserId == userId && o.Status == OrderStatus.Processing);

    if (existingOrder != null)
    {
        return existingOrder; // Return the existing order
    }
    else
    {
        // Create a new order with unique ID and other properties
        var newOrder = new Order
        {
            Id = Guid.NewGuid(), // Generate a new unique ID for the order
            UserId = userId,
            Status = OrderStatus.Processing,
            OrderItems = new List<OrderItem>() 
        };

        _orders.Add(newOrder);
        await _dbcontext.SaveChangesAsync();

        return newOrder;
    //   //  var cart = await _orders.Include(o => o.OrderItems).SingleOrDefaultAsync(o => o.UserId == userId   && o.Status== OrderStatus.Processing);

    //       //  if(cart is null){
    //            var  cart = new Order
    //             {
    //                 UserId = userId,
    //                 Status = OrderStatus.Processing,
    //                 OrderItems = new List<OrderItem>() 
    //             };
    //          //    _orders.Add(cart);
    //          // await _dbcontext.SaveChangesAsync();
    //     //    }
    //         return cart;
        
        }

       
        }
        
    }
}