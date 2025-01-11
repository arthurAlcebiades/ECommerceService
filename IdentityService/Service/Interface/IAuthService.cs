using IdentityService.Model.DTOs;

namespace IdentityService.Service.Interface;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDTO loginData);
    Task<bool> RegisterAsync(RegisterDTO dto);
}