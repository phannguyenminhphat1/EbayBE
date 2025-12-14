using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Models;


public class MapperToolInfras : Profile
{
    public MapperToolInfras()
    {
        CreateMap<Product, ProductEntity>().ReverseMap();
        CreateMap<ProductListCategory, ProductListCategoryEntity>().ReverseMap();
        CreateMap<ListingProductDetail, ListingProductDetailEntity>().ReverseMap();
        CreateMap<Category, CategoryEntity>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailEntity>().ReverseMap();
        CreateMap<Order, OrderEntity>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
        CreateMap<User, UserEntity>();
        CreateMap<UserRole, UserRoleEntity>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ReverseMap();

    }
}