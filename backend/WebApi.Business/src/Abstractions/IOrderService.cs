

using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Abstractions
{
    public interface IOrderService:IBaseService<Order,OrderCreateDto,OrderReadDto,OrderUpdateDto>
    {
        
    }
}