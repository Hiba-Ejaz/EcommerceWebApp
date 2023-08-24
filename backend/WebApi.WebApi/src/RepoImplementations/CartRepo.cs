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
            return cart;
        }
    }
}
