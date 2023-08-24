using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface ICartItemsRepo : IBaseRepo<CartItem>
    {
        Task<string> UpdateCartItem(CartItem cartItem);
        Task<CartItem?> GetCartItem(Guid cartId, Guid ProductId);
        Task<string> RemoveFromCart(Guid userIdGuid, Guid productId);
        Task<string> GetProductFromCartItem(CartItem cartItem);
        Task<IEnumerable<CartItem>> GetCartItems(Guid userId);
    }
}