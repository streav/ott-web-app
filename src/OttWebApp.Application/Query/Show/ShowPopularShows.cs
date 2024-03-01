using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetPopularShows : IRequest<IEnumerable<ShowDto>?>
{
    public class Handler : IRequestHandler<GetPopularShows, IEnumerable<ShowDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<ShowDto>?> Handle(GetPopularShows request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync("shows?sortBy=rating_desc&pageSize=10",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var list = await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowDto>>(cancellationToken);
            return list?.Data;
        }
    }
}