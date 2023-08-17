

using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller.src.Controllers
{
   
    public class CategoryController:CrudController<Category, CategoryCreateDto,CategoryReadDto,CategoryUpdateDto>
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService):base(categoryService)
        {
             _categoryService=categoryService;
        }
      [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _categoryService.GetAll(options));
        }

        [AllowAnonymous]
          public override async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetOneById([FromRoute] Guid id)
        {

                        return Ok(await _categoryService.GetOneById(id));
        }
        
        
    }
}