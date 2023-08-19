
using Microsoft.EntityFrameworkCore;
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