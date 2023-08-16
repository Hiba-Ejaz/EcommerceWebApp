
using AutoMapper;
using WebApi.Business.src.Dtos;
using WebApi.Domain.src.Entities;

namespace WebApi.WebApi.src.Configurations
{
    public class MapperProfile:Profile
    {

       public MapperProfile()
       {
        CreateMap<User,UserReadDto>();
        CreateMap<Product,ProductReadDto>();
        CreateMap<Category,CategoryReadDto>();
        CreateMap<Order,OrderReadDto>();
        CreateMap<UserReadDto,UserUpdateDto>();
        CreateMap<ProductReadDto,ProductUpdateDto>();
        CreateMap<CategoryReadDto,CategoryUpdateDto>();
        CreateMap<OrderReadDto,OrderUpdateDto>();
          CreateMap<UserCreateDto,UserReadDto>();
        CreateMap<ProductCreateDto,ProductReadDto>();
        CreateMap<Product,ProductUpdateDto>();
        CreateMap<CategoryCreateDto,CategoryReadDto>();
        CreateMap<OrderCreateDto,OrderReadDto>();
          CreateMap<UserCreateDto,User>();
          CreateMap<ProductCreateDto, Product>();
          CreateMap<ProductUpdateDto, Product>();
           CreateMap<AddToCartDto,ProductUpdateDto>();

       }
    }
}