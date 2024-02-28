using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OttWebApp.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
    }
}