using WebApi.Business.src.Dtos;

namespace WebApi.Business.src.Abstractions
{
    public interface ICartService
    {
        Task<string> AddToCart(Guid userId, AddToCartDto addToCartDto);
        Task<IEnumerable<CartReadDto>> GetCartItems(Guid userId);
        Task<string> RemoveFromCart(Guid userIdGuid, Guid productId);
        Task<string> DeleteCart(Guid userIdGuid);
    }
}