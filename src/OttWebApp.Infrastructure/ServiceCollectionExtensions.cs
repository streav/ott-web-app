using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OttWebApp.Infrastructure.EntityFramework;

namespace OttWebApp.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddEntityFramework(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => { options.UseSqlite(connectionString); });
    }
}