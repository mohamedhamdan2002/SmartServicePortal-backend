using SmartGallery.Core.Errors;
using SmartGallery.Service.Dtos.UserDtos;

namespace SmartGallery.Service.Contracts
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<bool> CheckIfEmailExists(string email);
    }
}
