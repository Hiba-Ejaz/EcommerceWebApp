
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class CartRepo : BaseRepo<Cart>, ICartRepo
    {
        private readonly DbSet<Cart> _carts;
        private readonly DatabaseContext _dbcontext;
        public CartRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _carts = dbContext.Carts;
            _dbcontext = dbContext;
        }
        public async Task<Cart> AddCart(Cart cart)
        {
            await _carts.AddAsync(cart);
                await _dbcontext.SaveChangesAsync();
                return cart;
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
public async Task<string> UpdateCart(Cart cart)
{
    try
    {
        _dbcontext.Entry(cart).State = EntityState.Modified;
        _carts.Update(cart);
        _dbcontext.Entry(cart).Collection(o => o.CartItems).IsModified = true;
        await _dbcontext.SaveChangesAsync();
        return "Cart updated successfully";
    }
    catch (Exception ex)
    {
        string errorMessage = $"Error updating cart ya yeh: {ex.Message}";
        if (ex.InnerException != null)
        {
            errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
        }
        return errorMessage;
    }
}
        public async Task<Cart?> GetProcessingCartByUserId(Guid userId)
        {
            var cart = await _carts
               .Include(o => o.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(o => o.UserId == userId);
                // && o.Status == OrderStatus.Processing);
               return cart;
        }
  

        public Task<bool> DeleteOneById(Cart entity)
        {
            throw new NotImplementedException();
        }

//      public async Task<string> RemoveFromCart(Guid userIdGuid, Guid productId)
// {
    
//     var order = await GetProcessingOrderByUserId(userIdGuid);
//     if (order == null)
//     {
//         return "order not found";
//     //    return false; // Order not found for the user
//     }
//     var orderId=order.Id;
//    if (order.OrderItems != null && order.OrderItems.Count > 0)
// {
    
//     var orderItemToRemove = order.OrderItems.FirstOrDefault(item =>
//       item.Order.Id == orderId  &&
//          item.Product.Id == productId);
//     if (orderItemToRemove is not null)
//     {
//         return "product  found";
//       //  return false; // Product not found in the user's cart
//     }
//      return "product not found";
// }
// return "order . orderitem null";
//     // order.OrderItems.Remove(orderItemToRemove);
//     // order.TotalAmount -= orderItemToRemove.Product.Price * orderItemToRemove.Quantity;
//     // _dbcontext.OrderItems.Remove(orderItemToRemove);

//     // await _dbcontext.SaveChangesAsync();
//     // return "order item removed"; 
// }

    }
    }
