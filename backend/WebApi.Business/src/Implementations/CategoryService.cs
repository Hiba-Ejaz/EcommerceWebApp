
using AutoMapper;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;

namespace WebApi.Business.src.Implementations
{
    public class CategoryService : BaseService<Category, CategoryCreateDto, CategoryReadDto, CategoryUpdateDto>, ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper) : base(categoryRepo, mapper)
        {
            _categoryRepo = categoryRepo;
        }
    }
}