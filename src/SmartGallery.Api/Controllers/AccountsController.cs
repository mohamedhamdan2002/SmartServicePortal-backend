using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.UserDtos;
using System.Security.Claims;

namespace SmartGallery.Api.Controllers
{
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

            //if (result.IsFailure)
            //    return HandleError(result.Error);

            //return Ok(result.GetData<UserDto>());
            return HandleResult<UserDto>(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserForLoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            //if (result.IsFailure)
            //    return HandleError(result.Error);

            //return Ok(result.GetData<UserDto>());
            return HandleResult<UserDto>(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetProfileForCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authService.GetUserByEmailAsync(email!);          
            return HandleResult<UserProfileDto>(result);
        }

        
    }
}
