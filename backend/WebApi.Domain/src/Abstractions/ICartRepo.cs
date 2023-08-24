using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface ICartRepo : IBaseRepo<Cart>
    {
        Task<string> UpdateCart(Cart cart);
        Task<Cart> AddCart(Cart cart);
        Task<Cart> GetProcessingCartByUserId(Guid userId);
    }
}