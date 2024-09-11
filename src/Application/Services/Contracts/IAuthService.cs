using Application.Dtos.UserDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IAuthService
{
    Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto);
    Task<Result> LoginAsync(UserForLoginDto userForLoginDto);
    Task<bool> CheckIfEmailExists(string email);
    Task<Result> GetUserByEmailAsync(string email);

}
