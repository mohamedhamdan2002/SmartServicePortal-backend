using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
using SmartGallery.Service.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Specifications
{
    public class UserSpecification : Specification<User, UserProfileDto>
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
}
