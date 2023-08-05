 
using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using AutoMapper;

namespace WebApi.Business.src.Implementations
{
    public class BaseService<T, TDto> : IBaseService<T, TDto>
    {
        private readonly IBaseRepo<T> _baseRepo;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepo<T> baseRepo,IMapper mapper)
        {
            _baseRepo=baseRepo;
            _mapper=mapper;
        }
          public bool DeleteOneById(string id)
        {
            var foundItem= _baseRepo.GetOneById(id);
            if(foundItem!=null){
            _baseRepo.DeleteOneById(foundItem);
            return true;
            }
            return false;
        }

         public IEnumerable<TDto> GetAll(SearchQueryOptions options)
        {
            return _mapper.Map<IEnumerable<TDto>>(_baseRepo.GetAll(options));
        }
    
        public TDto GetOneById(string id)
        {
            return _mapper.Map<TDto>(_baseRepo.GetOneById(id));
        }

        public TDto UpdateOneById(string id, TDto updated)
        {
            var foundItem=_baseRepo.GetOneById(id);
            if(foundItem is null){
                throw new Exception("Item not found");
            }
           var updatedEntity = _baseRepo.UpdateOne(foundItem,_mapper.Map<T>(updated));
            return _mapper.Map<TDto>(updatedEntity);
        }
    }
}