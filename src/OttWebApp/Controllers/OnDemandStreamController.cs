using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OttWebApp.Application.Dto.Stream;
using OttWebApp.Application.Query.OnDemandStream;

namespace OttWebApp.Controllers;

[Route("api/on-demand-stream")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class OnDemandStreamController
{
    private readonly IMediator _mediator;

    public OnDemandStreamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("urls")]
    public Task<IEnumerable<StreamUrlDto>?> Urls([FromBody] GetOnDemandStreamUrls query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }
}