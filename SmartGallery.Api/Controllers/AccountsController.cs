using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.UserDtos;

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
            
            if (result.IsFailure)
                return HandleError(result.Error);

            return Ok(result.GetData<UserDto>());
        }
    }
}
