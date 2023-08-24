using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IImageService
    {
         Task<Image> UploadImageAsync(Image image);
    }
}