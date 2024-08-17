using Microsoft.EntityFrameworkCore;
using SmartGallery.Api.Utilities;
using SmartGallery.Core.Repositories;
using SmartGallery.Repository;
using SmartGallery.Repository.Data;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Implementation;

namespace SmartGallery.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        ApiConstants.DefaultConnection
                     )
                );
            });
            return services;
        }
        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfigurePolicyCors(this IServiceCollection services)
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
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAppDbContext(configuration);
            services.ConfigurePolicyCors();
            services.ConfigureSwagger();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
        
    }
}
