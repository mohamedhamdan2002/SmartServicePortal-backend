using Api.Infrastructure;
using Api.Utilities;
using Application.Services.Contracts;
using Application.Services.Implementation;
using Application.Utilities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Data;
using System.Text;
using System.Runtime.CompilerServices;
using Application.Interfaces;
using Infrastructure.Hubs.Notification;
using Microsoft.AspNetCore.SignalR;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAppDbContext(configuration);
        services.ConfigureIdentity();
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.ConfigureJWT(configuration);
        services.ConfigurePolicyCors();
        services.ConfigureSwagger();
        services.ConfigureGlobalExcptionHandler();
        services.AddSignalR();
        //services.AddSingleton<IUserIdProvider>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IReviewService, ReviewService>();
        //services.AddScoped<INotificationHub, NotificationHub>();
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }
    private static void ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString(
                    Constants.DefaultConnection
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
            options.AddPolicy(Constants.MyAppPolicy, builder =>
            {
                builder.WithOrigins("http://localhost:4200", "http://localhost:4300")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });

    }
    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
    private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {

            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecreteKey))
            };
            opt.Events = new JwtBearerEvents
            {
                OnMessageReceived = (context) =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/api/notification-hub")
                    )
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }
    private static void ConfigureGlobalExcptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }
}
