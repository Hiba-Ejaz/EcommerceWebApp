using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
      public class ProductService : BaseService<Product, ProductCreateDto,ProductReadDto,ProductUpdateDto>, IProductService
    {
        private readonly IProductRepo _productRepository;
        public ProductService(IProductRepo productRepo, IMapper mapper) : base(productRepo, mapper)
        {
            _productRepository = productRepo;
        }

        public async Task<ProductUpdateDto> FindProductForUpdate(Guid id)
        {
          return _mapper.Map<ProductUpdateDto>(await _productRepository.GetOneById(id)); 
        }
    }


}