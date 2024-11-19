using Domain.Entities;

namespace Application.Dtos.UserDtos;

public record UserForRegisterDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Address { get; init; }
    public required string PhoneNumber { get; init; }

    public User ToEntity()
    {
        return new User
        {
            FirstName = this.FirstName,
            LastName = this.LastName,
            Email = this.Email,
            UserName = this.Email.Split('@')[0],
            PhoneNumber = this.PhoneNumber,
            Address = this.Address
        };
    }

}
