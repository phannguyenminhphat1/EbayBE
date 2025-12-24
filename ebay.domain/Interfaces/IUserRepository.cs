using ebay.domain.Entities;

namespace ebay.domain.Interfaces;

public interface IUserRepository
{
    Task<UserEntity?> FindUserByEmail(string email);

    Task UpdateMe(UserEntity user);

    string HashPassword(string password);

    bool ValidateHashPassword(string password, string passwordHash);

    Task AddUser(UserEntity userEntity);

    Task<UserEntity?> FindUserById(int id);

}