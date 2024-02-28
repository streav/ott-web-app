using IdentityModel.Client;
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

    public static void AddStreavHttpClient(this IServiceCollection services, string apiBaseUrl, string clientId,
        string clientSecret)
    {
        const string scopes =
            "api.live-stream.read api.movie.read api.show.read api.subscriber.read api.subscriber.create api.subscriber.update";

        services.AddAccessTokenManagement(options =>
        {
            options.Client.Clients.Add("streav", new ClientCredentialsTokenRequest
            {
                Address = apiBaseUrl + "oauth/token",
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scopes
            });
        });

        services.AddClientAccessTokenHttpClient("streav",
            configureClient: client => { client.BaseAddress = new Uri(apiBaseUrl + "v1"); });
    }
}