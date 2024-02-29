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

    [HttpGet("{id:int}")]
    public Task<ShowDto?> Index(int id, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetShow
        {
            Id = id
        }, cancellationToken);
    }

    [HttpGet("popular")]
    public Task<PaginatedListDto<ShowDto>?> Index(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetPopularShows(), cancellationToken);
    }

    [HttpGet("{id:int}/seasons")]
    public Task<IEnumerable<ShowSeasonDto>?> Seasons(int id, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetShowSeasons
        {
            Id = id
        }, cancellationToken);
    }

    [HttpGet("{id:int}/seasons/{number:int}")]
    public Task<IEnumerable<ShowEpisodeDto>?> Episodes(int id, int number, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetSeasonEpisodes
        {
            ShowId = id,
            SeasonNumber = number
        }, cancellationToken);
    }
}