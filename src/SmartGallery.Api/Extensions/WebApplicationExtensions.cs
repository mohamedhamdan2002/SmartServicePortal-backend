using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Repository.Data;
using System.Runtime.CompilerServices;

namespace SmartGallery.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public async static Task ApplyMigrationsAndSeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    await dbContext.Database.MigrateAsync();
                    await DataInitializer.InitializeAsync(dbContext);
               
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
}
