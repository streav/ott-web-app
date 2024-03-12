using System.Net.Http.Json;
using MediatR;
using Microsoft.Extensions.Configuration;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetPopularMovies : IRequest<IEnumerable<MovieDto>?>
{
    public class Handler : IRequestHandler<GetPopularMovies, IEnumerable<MovieDto>?>
    {
        private readonly HttpClient _client;
        private readonly int _bundleId;

        public Handler(IHttpClientFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient("streav");
            _bundleId = configuration.GetValue("Streav:BundleId", 1);
        }

        public async Task<IEnumerable<MovieDto>?> Handle(GetPopularMovies request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"movies?sortBy=rating_desc&pageSize=10&bundleId={_bundleId}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var list = await result.Content.ReadFromJsonAsync<PaginatedListDto<MovieDto>>(cancellationToken);
            return list?.Data;
        }
    }
}