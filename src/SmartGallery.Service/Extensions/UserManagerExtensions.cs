using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Core;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
namespace SmartGallery.Service.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<TResult?> GetUserWithSpec<TResult>(this UserManager<User> userManager, SpecificationWithResultType<User, TResult> specification)
        {
            return await userManager.Users.GetQuery(specification).FirstOrDefaultAsync();
        }
    }
}
