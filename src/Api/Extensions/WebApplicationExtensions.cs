using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Api.Extensions;

public static class WebApplicationExtensions
{
    public async static Task ApplyMigrationsAndSeedDataAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            //var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var roles = new[] { "admin", "manager", "customer" };
            try
            {
                //await dbContext.Database.MigrateAsync();
                //await DataInitializer.InitializeAsync(dbContext);
                //foreach (var role in roles)
                //{
                //    if (!await roleManager.RoleExistsAsync(role))
                //    {

                //        var result = roleManager.CreateAsync(new IdentityRole { Name = role });
                //    }
                //}

                //var identityRoles = roles.Select(role => new IdentityRole { Name = role, NormalizedName = roleManager.NormalizeKey(role) });
                //foreach (var role in identityRoles)
                //{
                //    await roleManager.UpdateNormalizedRoleNameAsync(role);
                //}
                //if (!dbContext.Roles.Any())
                //{
                //    dbContext.Roles.AddRange(identityRoles);
                //}
                //await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "this exception occur while applying Migrations to DataBase");
            }

        }
    }
}
