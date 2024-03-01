using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetLatestMovies : IRequest<PaginatedListDto<MovieDto>?>
{
    public class Handler : IRequestHandler<GetLatestMovies, PaginatedListDto<MovieDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }
        
        public async Task<PaginatedListDto<MovieDto>?> Handle(GetLatestMovies request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("movies?sortBy=releaseDate_desc&pageSize=10",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<PaginatedListDto<MovieDto>>(cancellationToken);
        }
    }
}