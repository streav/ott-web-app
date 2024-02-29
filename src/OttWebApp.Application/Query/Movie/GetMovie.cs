using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Movie;

public class GetMovie : IRequest<MovieDto?>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetMovie, MovieDto?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<MovieDto?> Handle(GetMovie request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"movies/{request.Id}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<MovieDto>(cancellationToken);
        }
    }
}