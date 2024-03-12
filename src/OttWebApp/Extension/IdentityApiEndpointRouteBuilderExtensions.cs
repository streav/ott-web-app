using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OttWebApp.Core.Entity;

namespace OttWebApp.Extension;

public record RegisterRequest(string Email, string Password, string Plan);

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to add identity endpoints.
/// </summary>
public static class IdentityApiEndpointRouteBuilderExtensions
{
    // Validate the email address using DataAnnotations like the UserValidator does when RequireUniqueEmail = true.
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    public static IEndpointConventionBuilder MapCustomIdentityApi(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup("/api/auth");

        routeGroup.MapPost("/register", async Task<Results<Ok, ValidationProblem>>
            ([FromBody] RegisterRequest registration, [FromServices] IServiceProvider sp) =>
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("streav");
            var configuration = sp.GetRequiredService<IConfiguration>();

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException(
                    $"{nameof(MapCustomIdentityApi)} requires a user store with email support.");
            }

            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;
            var email = registration.Email;

            if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
            {
                return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
            }

            var user = new User();
            await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return CreateValidationProblem(result);
            }

            var bundleId = configuration.GetValue("Streav:BundleId", 1);
            var subscriberId = await CreateSubscriberAsync(client, registration, bundleId);

            if (subscriberId.HasValue)
            {
                user.SubscriberId = subscriberId.Value;

                await userManager.UpdateAsync(user);
            }
            else
            {
                await userManager.DeleteAsync(user);

                return TypedResults.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "external", ["Cannot create a user at the moment."] }
                });
            }

            return TypedResults.Ok();
        });

        routeGroup.MapPost("/login", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>>
            ([FromBody] LoginRequest login, [FromServices] IServiceProvider sp) =>
        {
            var signInManager = sp.GetRequiredService<SignInManager<User>>();

            signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false,
                lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Empty;
        });

        routeGroup.MapGet("/info", async Task<Results<Ok<InfoResponse>, ValidationProblem, NotFound>>
            (ClaimsPrincipal claimsPrincipal, [FromServices] IServiceProvider sp) =>
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("streav");

            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(await CreateInfoResponseAsync(user, userManager, client));
        }).RequireAuthorization();

        routeGroup.MapPost("/logout", () => Results.Ok()).RequireAuthorization();

        return new IdentityEndpointsConventionBuilder(routeGroup);
    }

    private record SubscriberDto(int Id, DateTimeOffset? ExpiresAt, int? MaxConnections);

    private static async Task<int?> CreateSubscriberAsync(HttpClient client, RegisterRequest request, int bundleId)
    {
        var response = await client.PostAsJsonAsync("subscribers", new
        {
            request.Email,
            ExpiresAt = DateTimeOffset.UtcNow.AddMonths(1),
            MaxConnections = request.Plan.ToLower() switch
            {
                "basic" => 1,
                "standard" => 3,
                "premium" => 5,
                _ => 1
            },
            Note = "created by ott demo",
            Hls = true,
            MpegTs = true,
            Bundles = new[] { bundleId }
        });

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var data = await response.Content.ReadFromJsonAsync<SubscriberDto>();
        return data?.Id;
    }

    private static async Task<SubscriberDto?> GetSubscriberAsync(int id, HttpClient client)
    {
        var response = await client.GetAsync($"subscribers/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<SubscriberDto>();
    }

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        // We expect a single error code and description in the normal case.
        // This could be golfed with GroupBy and ToDictionary, but perf! :P
        Debug.Assert(!result.Succeeded);
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }

    public record InfoResponse(string? Email, DateTimeOffset? ExpiresAt, int? MaxConnections);

    private static async Task<InfoResponse> CreateInfoResponseAsync(User user, UserManager<User> userManager,
        HttpClient client)
    {
        SubscriberDto? subscriber = null;
        if (user.SubscriberId.HasValue)
        {
            subscriber = await GetSubscriberAsync(user.SubscriberId.Value, client);
        }

        var email = await userManager.GetEmailAsync(user) ??
                    throw new NotSupportedException("Users must have an email.");
        return new InfoResponse(email, subscriber?.ExpiresAt, subscriber?.MaxConnections);
    }

    private sealed class IdentityEndpointsConventionBuilder(RouteGroupBuilder inner) : IEndpointConventionBuilder
    {
        private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

        public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);

        public void Finally(Action<EndpointBuilder> finallyConvention) =>
            InnerAsConventionBuilder.Finally(finallyConvention);
    }
}