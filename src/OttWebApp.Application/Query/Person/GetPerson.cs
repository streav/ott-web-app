using System.Net.Http.Json;
using MediatR;
using OttWebApp.Application.Dto;

namespace OttWebApp.Application.Query.Person;

public class GetPerson : IRequest<PersonDto?>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetPerson, PersonDto?>
    {
        private readonly HttpClient _client;

        public Handler(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("streav");
        }

        public async Task<PersonDto?> Handle(GetPerson request, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync($"persons/{request.Id}",
                cancellationToken: cancellationToken);

            if (!result.IsSuccessStatusCode)
            {
                return default;
            }

            return await result.Content.ReadFromJsonAsync<PersonDto>(cancellationToken);
        }
    }
}