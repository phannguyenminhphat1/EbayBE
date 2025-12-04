using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;
using ebay.infrastructure.Models;


public class MapperToolInfras : Profile
{
    public MapperToolInfras()
    {
        CreateMap<ProductEntity, Product>().ReverseMap();
        CreateMap<Product, ProductEntity>().ReverseMap();
        CreateMap<User, UserEntity>()
            .ConstructUsing(u => new UserEntity(u.Username, u.PasswordHash, u.FullName!, u.Email, u.CreatedAt ?? DateTime.Now, u.Deleted))
            .AfterMap((src, dest) =>
            {
                if (src.UserRoles != null)
                {
                    foreach (var ur in src.UserRoles)
                    {
                        dest.AddRole(new UserRoleEntity(ur.UserId, ur.RoleId)
                        {
                            Description = ur.Description,
                            RoleId = ur.RoleId
                            // Nếu muốn map Role info thì thêm nữa
                        });
                    }
                }
            });
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<UserRoleEntity, UserRole>().ReverseMap();
        CreateMap<UserRole, UserRoleEntity>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();





    }
}