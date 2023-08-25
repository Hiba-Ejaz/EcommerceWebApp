using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class CartItemRepo : BaseRepo<CartItem>, ICartItemsRepo
    {
        private readonly DbSet<CartItem> _cartItem;
        private readonly DatabaseContext _dbcontext;
        public CartItemRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _cartItem = dbContext.CartItems;
            _dbcontext = dbContext;
        }
        public async Task<IEnumerable<CartItem>> GetCartItems(Guid userId)
        {
            var cartItems = await _cartItem
           .Where(oi => oi.Cart.UserId == userId)
           .Include(oi => oi.Product)
           .Include(oi => oi.Cart)
           .ToListAsync();
            return cartItems;
        }
        public async Task<CartItem?> GetCartItem(Guid cartId, Guid ProductId)
        { 
            return await _cartItem.FirstOrDefaultAsync(item =>
            item.Cart.Id == cartId && item.Product.Id == ProductId);
        }
        public async Task<string> GetProductFromCartItem(CartItem cartItem)
        {
            try
            {
                return cartItem.Product.Title;
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
        public async Task<string> RemoveFromCart(Guid userIdGuid, Guid productId)
        {
            var cartItemToRemove = await _cartItem
                .Include(oi => oi.Cart)
                .FirstOrDefaultAsync(oi =>
                    oi.Cart.UserId == userIdGuid &&
                    oi.Product.Id == productId);
            if (cartItemToRemove is not null)
            {
                var product = await _dbcontext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);
                var cart = cartItemToRemove.Cart;
                cart.CartItems.Remove(cartItemToRemove);
                if (cart is not null && product is not null)
                {
                    cart.TotalAmount -= product.Price * cartItemToRemove.Quantity;
                }
                _cartItem.Remove(cartItemToRemove);
                await _dbcontext.SaveChangesAsync();
                return "product removed from cart";
            }
            return "product not found in cart";
        }
        public async Task<string> UpdateCartItem(CartItem cartItem)
        {
            try
            {
                _cartItem.Update(cartItem);
                await _dbcontext.SaveChangesAsync();
                return " cart item updated successfully";
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error updating cart: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" (Inner Exception: {ex.InnerException.Message})";
                }
                return errorMessage;
            }
        }
    }
}