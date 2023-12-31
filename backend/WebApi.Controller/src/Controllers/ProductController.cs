using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controller.src.Controllers
{
  //[Authorize] 
  public class ProductController : CrudController<Product, ProductCreateDto, ProductReadDto, ProductUpdateDto>
  {
    private readonly IProductService _productService;
    public ProductController(IProductService productService) : base(productService)
    {
      _productService = productService;
    }
    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
    {
      return Ok(await _productService.GetAll(options));
    }

    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ProductReadDto>>> GetOneById([FromRoute] Guid id)
    {

      return Ok(await _productService.GetOneById(id));
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ProductReadDto>> CreateOne([FromBody] ProductCreateDto created)
    {
      var createdObject = await _productService.CreateOne(created);
      return CreatedAtAction(nameof(CreateOne), createdObject);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:Guid}")]
    public override async Task<ActionResult<ProductReadDto>> DeleteOneById([FromRoute] Guid id)
    {
      return Ok(await _productService.DeleteOneById(id));
    }

    [HttpGet("{id:Guid}/update")]
    public async Task<ActionResult<ProductUpdateDto?>> FindProductByIdForUpdate([FromRoute] Guid id)
    {
      return Ok(await _productService.FindProductForUpdate(id));
    }

    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ProductReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] ProductUpdateDto updated)
    {
      return Ok(await _productService.UpdateOneById(id, updated));
    }
  }
}