using IdentityService.Model.DTOs;
using IdentityService.Service.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("/api/login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var token = await _authService.LoginAsync(dto);
        return Ok(new { Token = token });
    }

    [HttpPost("/api/register-user")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
        var isCreated = await _authService.RegisterAsync(dto);
        if(isCreated)
            return Ok();

        return StatusCode(StatusCodes.Status400BadRequest);
    }
}