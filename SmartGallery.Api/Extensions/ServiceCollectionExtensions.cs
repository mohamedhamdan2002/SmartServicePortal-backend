using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Api.Utilities;
using SmartGallery.Core.Entities;
using SmartGallery.Core.Repositories;
using SmartGallery.Repository;
using SmartGallery.Repository.Data;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Implementation;

namespace SmartGallery.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAppDbContext(configuration);
            services.ConfigureIdentity();
            services.ConfigurePolicyCors();
            services.ConfigureSwagger();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReservationService, ReservationService>();
            return services;
        }
        private static void ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        ApiConstants.DefaultConnection
                     )
                );
            });
        }
        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        private static void ConfigurePolicyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(ApiConstants.MyAppPolicy, builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
         
        }
        private static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }



    }
}
