using Domain.Entities;
using Domain.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<TResult?> GetUserWithSpec<TResult>(this UserManager<User> userManager, Specification<User, TResult> specification)
        => await userManager.Users.GetQuery(specification).FirstOrDefaultAsync();
}
