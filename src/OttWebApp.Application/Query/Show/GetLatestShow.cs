using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.Configuration;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetLatestShow : IRequest<ShowDto?>
{
    public class Handler : IRequestHandler<GetLatestShow, ShowDto?>
    {
        private readonly HttpClient _client;
        private readonly int _bundleId;

        public Handler(IHttpClientFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient("streav");
            _bundleId = configuration.GetValue("Streav:BundleId", 1);
        }

        public async Task<ShowDto?> Handle(GetLatestShow request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"shows?sortBy=releaseDate_desc&pageSize=1&bundleId={_bundleId}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var movies = await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowDto>>(cancellationToken);
            return movies?.Data?.FirstOrDefault();
        }
    }
}