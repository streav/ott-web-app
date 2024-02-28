using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetPopularShows : IRequest<PaginatedListDto<ShowDto>?>
{
    public class Handler : IRequestHandler<GetPopularShows, PaginatedListDto<ShowDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<PaginatedListDto<ShowDto>?> Handle(GetPopularShows request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("shows?sortBy=rating_desc&pageSize=10",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowDto>>(cancellationToken);
        }
    }
}