using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetPopularMovies : IRequest<IEnumerable<MovieDto>?>
{
    public class Handler : IRequestHandler<GetPopularMovies, IEnumerable<MovieDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<MovieDto>?> Handle(GetPopularMovies request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("movies?sortBy=rating_desc&pageSize=10",
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