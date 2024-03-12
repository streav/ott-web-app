using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.Configuration;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetLatestMovie : IRequest<MovieDto?>
{
    public class Handler : IRequestHandler<GetLatestMovie, MovieDto?>
    {
        private readonly HttpClient _client;
        private readonly int _bundleId;

        public Handler(IHttpClientFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient("streav");
            _bundleId = configuration.GetValue("Streav:BundleId", 1);
        }

        public async Task<MovieDto?> Handle(GetLatestMovie request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"movies?sortBy=releaseDate_desc&pageSize=1&bundleId={_bundleId}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var movies = await result.Content.ReadFromJsonAsync<PaginatedListDto<MovieDto>>(cancellationToken);
            return movies?.Data?.FirstOrDefault();
        }
    }
}