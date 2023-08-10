
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
    [Authorize] 
    public class ProductController:CrudController<Product, ProductCreateDto,ProductReadDto,ProductUpdateDto>
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService):base(productService)
        {
             _productService=productService;
        }
        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _productService.GetAll(options));
        }

        [AllowAnonymous]
          public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetOneById([FromRoute] string id)
        {

                        return Ok(await _productService.GetOneById(id));
        }
    }


}