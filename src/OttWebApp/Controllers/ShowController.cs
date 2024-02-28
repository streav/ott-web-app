using MediatR;
using Microsoft.AspNetCore.Mvc;
using OttWebApp.Application.Dto;
using OttWebApp.Application.Query.Show;

namespace OttWebApp.Controllers;

[Route("api/shows")]
[ApiController]
[Produces("application/json")]
public class ShowController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShowController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("popular")]
    public Task<PaginatedListDto<ShowDto>?> Index(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetPopularShows(), cancellationToken);
    }
}