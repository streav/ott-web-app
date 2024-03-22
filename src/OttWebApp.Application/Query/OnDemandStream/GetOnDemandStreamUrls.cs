using System.Net.Http.Json;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OttWebApp.Application.Dto.Stream;
using OttWebApp.Core.Entity;

namespace OttWebApp.Application.Query.OnDemandStream;

public class GetOnDemandStreamUrls : IRequest<IEnumerable<StreamUrlDto>?>
{
    public int StreamId { get; set; }

    public class Handler : IRequestHandler<GetOnDemandStreamUrls, IEnumerable<StreamUrlDto>?>
    {
        private readonly UserManager<User> _userManager;
        private readonly HttpClient _client;
        private readonly HttpContext? _httpContext;

        public Handler(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _client = factory.CreateClient("streav");
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<IEnumerable<StreamUrlDto>?> Handle(GetOnDemandStreamUrls request,
            CancellationToken cancellationToken)
        {
            if (_httpContext is null)
            {
                return default;
            }

            var user = await _userManager.GetUserAsync(_httpContext.User);
            if (user?.SubscriberId == null)
            {
                return default;
            }

            const string url = "on-demand-streams/urls";

            var result = await _client.PostAsJsonAsync(url, new
            {
                request.StreamId,
                user.SubscriberId,
                Https = true
            }, cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<IEnumerable<StreamUrlDto>>(cancellationToken);
        }
    }
}