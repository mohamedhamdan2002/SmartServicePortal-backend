using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.UserDtos;

namespace SmartGallery.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            var isEmailExists = await CheckIfEmailExists(userForRegisterDto.Email);
            if (isEmailExists)
                return ApplicationErrors.BadRequestError;

            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                UserName = userForRegisterDto.Email.Split('@')[0],
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Address = userForRegisterDto.Address
            };
            var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
            if (!result.Succeeded)
                return ApplicationErrors.BadRequestError;

            var userToReturn = new UserDto
            {
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = "klajsklfjalkjflakjdfkjakldfjlaksfjkladsjfklasj"
            };
            return Result<UserDto>.Success(userToReturn);
        }

        public async Task<bool> CheckIfEmailExists(string email)
            => await _userManager.Users.AnyAsync(user => user.Email == email);

        public async Task<Result> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
            if (user is null) return ApplicationErrors.UnauthorizedError;
            var result = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);
            if (!result)
                return ApplicationErrors.UnauthorizedError;

            var userToReturn = new UserDto
            {
                Email = user.Email!,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = "klajsklfjalkjflakjdfkjakldfjlaksfjkladsjfklasj"
            };
            return Result<UserDto>.Success(userToReturn);
        }
    }
}
