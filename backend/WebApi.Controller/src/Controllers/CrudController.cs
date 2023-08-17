
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
     [Route("api/v1/[controller]s")]

    public class CrudController<T,TCreateDto,TReadDto,TUpdateDto> : ControllerBase
    {
        private readonly IBaseService<T,TCreateDto,TReadDto,TUpdateDto> _baseservice;
        public CrudController(IBaseService<T,TCreateDto,TReadDto,TUpdateDto> service)
        {
            _baseservice = service;
        }

        [HttpGet] 
        public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _baseservice.GetAll(options));
        }
        [HttpGet("{id:Guid}")]
         public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetOneById([FromRoute] Guid id)
        {

                        return Ok(await _baseservice.GetOneById(id));
        }
       
        [HttpDelete("{id:Guid}")]
         public async Task<ActionResult<TReadDto>> DeleteOneById([FromRoute] Guid id){
            return Ok(await _baseservice.DeleteOneById(id));
         }
         [HttpPost]
         
       public virtual async Task<ActionResult<TReadDto>> CreateOne([FromBody] TCreateDto created){
            var createdObject=await _baseservice.CreateOne(created);
            return CreatedAtAction(nameof(CreateOne),createdObject);
         }
    }
}