using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetShowSeasons : IRequest<IEnumerable<ShowSeasonDto>?>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetShowSeasons, IEnumerable<ShowSeasonDto>?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<IEnumerable<ShowSeasonDto>?> Handle(GetShowSeasons request,
            CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"shows/{request.Id}/seasons?pageSize=100",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            var paginatedList =
                await result.Content.ReadFromJsonAsync<PaginatedListDto<ShowSeasonDto>>(cancellationToken);

            return paginatedList?.Data;
        }
    }
}