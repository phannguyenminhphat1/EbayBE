using AutoMapper;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;

namespace ebay.infrastructure.Repositories;

public class UserRoleRepository(EBayDbContext _context, IMapper _mapper) : IUserRoleRepository
{
    public async Task AddUserRole(UserRoleEntity userRoleEntity)
    {
        var userRole = new UserRole
        {
            UserId = userRoleEntity.UserId,
            RoleId = userRoleEntity.RoleId
        };
        await _context.UserRoles.AddAsync(userRole);
    }

}