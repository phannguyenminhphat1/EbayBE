using System.Text.Json;
using AutoMapper;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using ebay.infrastructure.Data;
using ebay.infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ebay.infrastructure.Repositories;

public class UserRepository(EBayDbContext _context, IMapper _mapper) : IUserRepository
{
    #region FIND USER BY ID
    public async Task<UserEntity?> FindUserById(int id)
    {
        var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstAsync(u => u.Id == id && u.Deleted == false);
        if (user == null) return null;
        var userMapper = _mapper.Map<UserEntity>(user);
        return userMapper;
    }
    #endregion

    #region FIND USER BY EMAIL
    public async Task<UserEntity?> FindUserByEmail(string email)
    {
        var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).SingleOrDefaultAsync(u => u.Email == email && u.Deleted == false);
        if (user == null) return null;
        var userMapper = _mapper.Map<UserEntity>(user);
        return userMapper;
    }
    #endregion

    #region HASH PASSWORD
    public string HashPassword(string password)
    {
        int workFactor = 12;
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
    }
    #endregion

    #region ADD USER
    public async Task AddUser(UserEntity userEntity)
    {
        var user = new User
        {
            Username = userEntity.Username,
            Email = userEntity.Email,
            PasswordHash = userEntity.PasswordHash,
            FullName = userEntity.FullName,
            CreatedAt = userEntity.CreatedAt,
            Deleted = userEntity.Deleted,
            Ava = userEntity.Ava,

        };


        foreach (var role in userEntity.UserRoles)
        {
            user.UserRoles.Add(new UserRole
            {
                RoleId = role.RoleId,
                Description = role.Description
            });
        }
        await _context.Users.AddAsync(user);
        System.Console.WriteLine(JsonSerializer.Serialize(user.Id));
    }
    #endregion

    #region VALIDATE HASH PASSWORD
    public bool ValidateHashPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public async Task UpdateMe(UserEntity userEntity)
    {
        var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).SingleAsync(u => u.Id == userEntity.Id && u.Deleted == false);
        if (user == null) return;
        user.Address = userEntity.Address;
        user.PasswordHash = userEntity.PasswordHash;
        user.FullName = userEntity.FullName;
        user.Ava = userEntity.Ava;
        user.Phone = userEntity.Phone;
        user.Address = userEntity.Address;
        _context.Users.Update(user);

    }
    #endregion

    #region Add Role To User
    public async Task AddRoleToUser(int userId, int roleId)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == userId && u.Deleted == false);

        if (user == null) return;

        if (user.UserRoles.Any(r => r.RoleId == roleId)) return;

        user.UserRoles.Add(new UserRole
        {
            RoleId = roleId
        });
    }


    #endregion

}