using Application.Dtos.UserDtos;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Specifications;

public class UserSpecification : SpecificationWithResultType<User, UserProfileDto>
{
    public UserSpecification(string email) : base(user => user.Email == email)
    {
        Selector = user => new UserProfileDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = email,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber
        };
    }
}
