using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetShows : IRequest<IEnumerable<ShowDto>?>
{
    public string? Name { get; set; }
    public int Page { get; set; }
    public string? SortBy { get; set; }

    public class Handler : IRequestHandler<GetShows, IEnumerable<ShowDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<ShowDto>?> Handle(GetShows request, CancellationToken cancellationToken)
        {
            request.SortBy = string.IsNullOrWhiteSpace(request.SortBy) ? "releaseDate_desc" : request.SortBy;

            var url = $"shows?title={request.Name}&sortBy={request.SortBy}&page={request.Page}&pageSize=12";
            
            var result = await _client.GetAsync(url, cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var list = await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowDto>>(cancellationToken);

            return list?.Data;
        }
    }
}