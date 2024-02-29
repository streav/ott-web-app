using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetSeasonEpisodes : IRequest<IEnumerable<ShowEpisodeDto>?>
{
    public int ShowId { get; set; }
    public int SeasonNumber { get; set; }

    public class Handler : IRequestHandler<GetSeasonEpisodes, IEnumerable<ShowEpisodeDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<ShowEpisodeDto>?> Handle(GetSeasonEpisodes request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"shows/{request.ShowId}/seasons/{request.SeasonNumber}?pageSize=100",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var paginatedList =
                await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowEpisodeDto>>(cancellationToken);

            return paginatedList?.Data;
        }
    }
}