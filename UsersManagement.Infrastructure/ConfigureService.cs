using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Application.Interfaces.Repositories;
using UsersManagement.Application.Interfaces.Services;
using UsersManagement.Application.Queries.Handlers;
using UsersManagement.Infrastructure.Repositories;
using UsersManagement.Infrastructure.Services;
using UsersManagement.Persistence.DbContext;

namespace UsersManagement.Infrastructure;

public static class ConfigureService
{

    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext(configuration)
            .AddServices()
            .AddRepositories();
    }

  
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("UserManagement")));

        return services;

    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IUserProfileUpdatesService, UserProfileUpdatesService>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<UserSuspensionService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IUserProfileUpdatesRepository, UserProfileUpdatesRepository>();
        
        return services;
    }
}