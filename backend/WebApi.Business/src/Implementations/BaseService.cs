using WebApi.Business.src.Abstractions;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using AutoMapper;
using WebApi.Business.src.Implementations.Shared;

namespace WebApi.Business.src.Implementations
{
    public abstract class BaseService<T, TCreateDto, TReadDto, TUpdateDto> : IBaseService<T, TCreateDto, TReadDto, TUpdateDto>
    {
        private readonly IBaseRepo<T> _baseRepo;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepo<T> baseRepo, IMapper mapper)
        {
            _baseRepo = baseRepo;
            _mapper = mapper;
        }
        public async Task<bool> DeleteOneById(Guid id)
        {
            var foundItem = await _baseRepo.GetOneById(id);
            if (foundItem != null)
            {
                await _baseRepo.DeleteOneById(foundItem);
                return true;
            }
           throw CustomException.NotFoundException();
        }
        public virtual async Task<IEnumerable<TReadDto>> GetAll(SearchQueryOptions options)
        {
            var mappedResult = _mapper.Map<IEnumerable<TReadDto>>(await _baseRepo.GetAll(options)) ?? throw CustomException.NotFoundException();
            return mappedResult;
        }
        public async Task<TReadDto> GetOneById(Guid id)
        {
            var mappedResult= _mapper.Map<TReadDto>(await _baseRepo.GetOneById(id)) ?? throw CustomException.NotFoundException(); //writing await bcz all functions in base repo have task as return type. 
            return mappedResult;
        }
        public virtual async Task<TReadDto> CreateOne(TCreateDto dto)
        {
            var createdEntity = await _baseRepo.CreateOne(_mapper.Map<T>(dto));
            return _mapper.Map<TReadDto>(createdEntity);
        }
    }
}