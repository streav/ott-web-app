using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Show;

public class GetShow : IRequest<ShowDto?>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetShow, ShowDto?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<ShowDto?> Handle(GetShow request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"shows/{request.Id}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<ShowDto>(cancellationToken);
        }
    }
}