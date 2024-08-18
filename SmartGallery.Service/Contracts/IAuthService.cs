using SmartGallery.Core.Errors;
using SmartGallery.Service.Dtos.UserDtos;
using System.Security.Claims;

namespace SmartGallery.Service.Contracts
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<Result> LoginAsync(UserForLoginDto userForLoginDto);
        Task<bool> CheckIfEmailExists(string email);
    }
}
