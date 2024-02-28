using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetLatestMovie : IRequest<MovieDto?>
{
    public class Handler : IRequestHandler<GetLatestMovie, MovieDto?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<MovieDto?> Handle(GetLatestMovie request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("movies?sortBy=releaseDate_desc&pageSize=1",
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