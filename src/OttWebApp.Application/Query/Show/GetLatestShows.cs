using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetLatestShows : IRequest<PaginatedListDto<ShowDto>?>
{
    public class Handler : IRequestHandler<GetLatestShows, PaginatedListDto<ShowDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }
        
        public async Task<PaginatedListDto<ShowDto>?> Handle(GetLatestShows request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("shows?sortBy=releaseDate_desc&pageSize=10",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowDto>>(cancellationToken);
        }
    }
}