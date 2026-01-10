using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;


public class MapperTool : Profile
{
    public MapperTool()
    {
        CreateMap<ProductListCategoryEntity, ProductListCategoryDto>().ReverseMap();
        CreateMap<ListingProductDetailEntity, GetListingProductDetailDto>().ReverseMap();
        CreateMap<ProductListCategoryEntity, ProductsByCategoryIdDto>().ReverseMap();
        CreateMap<UserRoleEntity, UserRoleDto>().ReverseMap();
        CreateMap<OrderStatisticsReadModel, GetOrderStatisticsDto>().ReverseMap();
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserEntity, GetMeDto>();
        CreateMap<CategoryEntity, CategoryDto>();

    }
}