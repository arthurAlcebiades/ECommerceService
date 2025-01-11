using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Model.DTOs;
using IdentityService.Model.Entities;
using IdentityService.Repository.Interface;
using IdentityService.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Service.Implementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _jwtKey;

    public AuthService(IUserRepository userRepository, string jwtKey)
    {
        _userRepository = userRepository;
        _jwtKey = jwtKey;
    }
    
    public async Task<string> LoginAsync(LoginDTO loginData)
    {
        var user = await _userRepository.GetUserByEmail(loginData.Email);
        if (user is null || user?.PasswordHash != loginData?.Password)
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        return GenerateJwtToken(user);
    }

    public async Task<bool> RegisterAsync(RegisterDTO dto)
    {
        var existingUser = await _userRepository.VerifyUserExist(dto.Email);
        if (existingUser)
            throw new InvalidOperationException("Usuário já existe.");
        
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = dto.PasswordHash,
            Email = dto.Email,
            Cnpj = dto.Cnpj,
            Cpf = dto.Cpf,
            CreeatedAt = DateTime.UtcNow,
            IsActive = true,
            PhoneNumber = dto.PhoneNumber
        };
        
        var isCreated = await _userRepository.RegisterUser(user);
        if (!isCreated)
            throw new Exception("Não foi possível registrar usuário");

        return isCreated;
    }
    
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}