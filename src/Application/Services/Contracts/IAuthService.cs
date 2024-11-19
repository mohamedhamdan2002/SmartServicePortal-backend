using Application.Dtos.UserDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IAuthService
{
    Task<Result<UserDto>> RegisterAsync(UserForRegisterDto userForRegisterDto);
    Task<Result> LoginAsync(UserForLoginDto userForLoginDto);
    Task<bool> CheckIfEmailExists(string email);
    Task<Result<UserProfileDto>> GetUserByEmailAsync(string email);
    Task<Result> ConfirmEmailAsync(string email, string confirmToken);

}
