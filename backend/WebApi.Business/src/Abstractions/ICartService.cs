

using WebApi.Business.src.Dtos;

namespace WebApi.Business.src.Abstractions
{
    public interface ICartService
    {
            
        Task<string> AddToCart(Guid userId,AddToCartDto addToCartDto);
    

    }
}