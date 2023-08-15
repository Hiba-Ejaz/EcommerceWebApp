
using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class ImageService : IImageService
    {
         private readonly IImageRepo _imageRepository;

        public ImageService(IImageRepo imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image> UploadImageAsync(Image image)
        {
            return await _imageRepository.AddImageAsync(image);
        }
    }
}