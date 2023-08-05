using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
      public class ProductService : BaseService<Product, ProductDto>, IProductService
    {
        private readonly IProductRepo _productRepository;
        public ProductService(IProductRepo productRepo, IMapper mapper) : base(productRepo, mapper)
        {
            _productRepository = productRepo;
        }
    }


}