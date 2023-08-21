
using System.Net.Security;
using Microsoft.EntityFrameworkCore;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class OrderItemRepo:BaseRepo<OrderItem>,IOrderItemsRepo
    {
        private readonly DbSet<OrderItem> _orderItem;
        private readonly DatabaseContext _dbcontext;

 


        public OrderItemRepo(DatabaseContext dbContext) : base(dbContext)
        {
             _orderItem=dbContext.OrderItems;
            _dbcontext=dbContext;
        }

        public async Task<IEnumerable<OrderItem>> GetCartItems(Guid userId){
       var orderItems = await _orderItem
        .Where(oi => oi.Order.UserId == userId)
        .Include(oi => oi.Product)
        .Include(oi => oi.Order)
        .ToListAsync();
        return orderItems;
        }

        public async Task<OrderItem?> GetOrderItem(Guid orderId,Guid ProductId){
        return await _orderItem.FirstOrDefaultAsync(item =>
        item.Order.Id == orderId && item.Product.Id == ProductId);
        }

        ///
         public async Task<string> RemoveFromCart(Guid userIdGuid, Guid productId)
{
    var orderItemToRemove = await _orderItem
        .Include(oi => oi.Order) 
        .FirstOrDefaultAsync(oi =>
            oi.Order.UserId == userIdGuid &&
            oi.Product.Id == productId);
               if (orderItemToRemove is not null)
    {
        var product = await _dbcontext.Products
            .FirstOrDefaultAsync(p => p.Id == productId);
         var order=orderItemToRemove.Order;
         order.OrderItems.Remove(orderItemToRemove);
          if(order is not null && product is not null){
         order.TotalAmount -= product.Price * orderItemToRemove.Quantity;
          }
        _orderItem.Remove(orderItemToRemove); // Remove the order item
        await _dbcontext.SaveChangesAsync();
        return "product removed from cart";
    }

    return "product not found in cart";
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
}
/// <summary>
/// //////////////////
/// </summary>

        
        public async Task<string> UpdateOrderItem(OrderItem orderItem)
        {
       try
         {
        _orderItem.Update(orderItem);
        await _dbcontext.SaveChangesAsync();
        return " order item updated successfully";
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
    }
}