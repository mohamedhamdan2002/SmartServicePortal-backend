using Application.Dtos.UserDtos;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

public class AccountsController : BaseApiController
{
    private readonly IAuthService _authService;
    public AccountsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] UserForRegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);
        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] UserForLoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return HandleResult(result);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileDto>> GetProfileForCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await _authService.GetUserByEmailAsync(email!);
        return HandleResult(result);
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
    {
        var result = await _authService.ConfirmEmailAsync(email, token);
        return HandleResult(result);
    }
}
