
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CrudController<T,TCreateDto,TReadDto,TUpdateDto> : ControllerBase
    {
        private readonly IBaseService<T,TCreateDto,TReadDto,TUpdateDto> _service;
        public CrudController(IBaseService<T,TCreateDto,TReadDto,TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet] 
        public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetAll([FromQuery] SearchQueryOptions options)
        {
                        return Ok(await _service.GetAll(options));
        }
        [HttpGet("{id}")]
         public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetOneById([FromRoute] string id)
        {

                        return Ok(await _service.GetOneById(id));
        }
        [HttpPatch("{id}")]
         public async Task<ActionResult<TReadDto>> UpdateOneById([FromRoute] string id, [FromBody] TUpdateDto updated){
            return Ok(await _service.UpdateOneById(id, updated));
         }
        [HttpDelete("{id}")]
         public async Task<ActionResult<TReadDto>> DeleteOneById([FromRoute] string id){
            return Ok(await _service.DeleteOneById(id));
         }
         [HttpPost]
         public async Task<ActionResult<TReadDto>> CreateOne([FromBody] TCreateDto created){
            var createdObject=await _service.CreateOne(created);
            return CreatedAtAction("created",createdObject);
         }



    }
}