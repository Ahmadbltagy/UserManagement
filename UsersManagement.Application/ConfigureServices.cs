using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace UsersManagement.Application;

public static class ConfigureServices
{

   
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}