using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetPopularMovies : IRequest<PaginatedListDto<MovieDto>?>
{
    public class Handler : IRequestHandler<GetPopularMovies, PaginatedListDto<MovieDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<PaginatedListDto<MovieDto>?> Handle(GetPopularMovies request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("movies?sortBy=rating_desc&pageSize=10",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<PaginatedListDto<MovieDto>>(cancellationToken);
        }
    }
}