using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;

using SmartGallery.Core;
namespace SmartGallery.Service.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<TResult?> GetUserWithSpec<TResult>(this UserManager<User> userManager, ISpecification<User, TResult> specification)
        {
           return await userManager.Users.GetQuery(specification).FirstOrDefaultAsync();
        }
    }
}
