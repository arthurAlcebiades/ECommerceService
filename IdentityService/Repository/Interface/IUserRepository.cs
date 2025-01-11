using IdentityService.Model.Entities;

namespace IdentityService.Repository.Interface;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<bool> RegisterUser(User user);
    Task<bool> VerifyUserExist(string email);
}