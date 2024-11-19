using Api.Utilities;
using Application.Dtos.UserDtos;
using Application.Extensions;
using Application.Services.Contracts;
using Application.Specifications;
using Application.Utilities;
using Domain.Entities;
using Domain.Errors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private IDataProtector _dataProtector;
    public AuthService(
        UserManager<User> userManager, 
        IOptions<JwtSettings> options,
        IDataProtectionProvider provider
    )
    {
        _userManager = userManager;
        _jwtSettings = options.Value;
        _dataProtector = provider.CreateProtector("MySmartServicesPortal");
    }

    public async Task<Result<UserDto>> RegisterAsync(UserForRegisterDto userForRegisterDto)
    {
        var isEmailExists = await CheckIfEmailExists(userForRegisterDto.Email);
        if (isEmailExists)
            return Result.Fail<UserDto>(ApplicationErrors.BadRequestError);

        var user = userForRegisterDto.ToEntity();
        var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
        if (!result.Succeeded)
            return Result.Fail<UserDto>(ApplicationErrors.BadRequestError);

        var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        var protectedEmail = _dataProtector.Protect(user.Email);
        var protedtedToken = _dataProtector.Protect(encodedToken);
        var confirmUrl = $"${Constants.BaseUrl}confirm-email?email={protectedEmail}&token={protedtedToken}";

        var userToReturn = new UserDto
        {
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Token = await GenerateTokenAsync(user)
        };
        return Result.Success(userToReturn);
    }

    public async Task<bool> CheckIfEmailExists(string email)
        => await _userManager.Users.AnyAsync(user => user.Email == email);

    public async Task<Result> LoginAsync(UserForLoginDto userForLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
        if (user is null) return Result.Fail(ApplicationErrors.UnauthorizedError);
        //if (!await _userManager.IsEmailConfirmedAsync(user))
        //    return ApplicationErrors.BadRequestError;

        var result = await _userManager.CheckPasswordAsync(user, userForLoginDto.Password);
        if (!result)
            return Result.Fail(ApplicationErrors.UnauthorizedError);

        var userToReturn = new UserDto
        {
            Email = user.Email!,
            FullName = $"{user.FirstName} {user.LastName}",
            Token = await GenerateTokenAsync(user)
        };
        return Result.Success(userToReturn);
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
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id)
        };
        var roles = await _userManager.GetRolesAsync(user);
        if (roles?.Count > 0)
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

    public async Task<Result<UserProfileDto>> GetUserByEmailAsync(string email)
    {
        if (email is null)
            return Result.Fail<UserProfileDto>(ApplicationErrors.BadRequestError);
        var spec = new UserSpecification(email);
        var user = await _userManager.GetUserWithSpec(spec);
        if (user is null)
            return Result.Fail<UserProfileDto>(ApplicationErrors.BadRequestError);
        return Result.Success(user);
    }

    public async Task<Result> ConfirmEmailAsync(string email, string confirmToken)
    {
        var userEmail = _dataProtector.Unprotect(email);
        var encodedToken = _dataProtector.Unprotect(confirmToken);
        var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user is null) return Result.Fail(ApplicationErrors.NotFoundError);
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded ? Result.Success() : Result.Fail(ApplicationErrors.BadRequestError);
    }
}