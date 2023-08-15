

using WebApi.Domain.src.Entities;

namespace WebApi.Domain.src.Abstractions
{
    public interface IImageRepo
    {
         Task<Image> AddImageAsync(Image image);
    }
}