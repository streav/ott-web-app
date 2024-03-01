using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetMovies : IRequest<IEnumerable<MovieDto>?>
{
    public string? Name { get; set; }
    public int Page { get; set; }
    public string? SortBy { get; set; }

    public class Handler : IRequestHandler<GetMovies, IEnumerable<MovieDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<MovieDto>?> Handle(GetMovies request, CancellationToken cancellationToken)
        {
            request.SortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "releaseDate_desc" : request.SortBy;

            var url = $"movies?title={request.Name}&sortBy={request.SortBy}&page={request.Page}&pageSize=12";

            var result = await _client.GetAsync(url, cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var list = await result.Content.ReadFromJsonAsync<PaginatedListDto<MovieDto>>(cancellationToken);

            return list?.Data;
        }
    }
}