using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.UserDtos;
using SmartGallery.Service.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartGallery.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        public AuthService(UserManager<User> userManager, IOptions<JwtSettings> options)
        {
            _userManager = userManager;
            _jwtSettings = options.Value;
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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user)
            };
            return Result<UserDto>.Success(userToReturn);
        }
        private async Task<string> GenerateTokenAsync(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(claims, signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecreteKey);
            var symmetricKey = new SymmetricSecurityKey(key);
            return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
        }
        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("uid", user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            if(roles?.Count > 0)
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(IEnumerable<Claim> claims, SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                    issuer: _jwtSettings.ValidIssuer,
                    audience: _jwtSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_jwtSettings.Expire),
                    signingCredentials: signingCredentials
                );
        }
    }
}
