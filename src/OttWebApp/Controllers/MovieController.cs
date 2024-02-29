using MediatR;
using Microsoft.AspNetCore.Mvc;
using OttWebApp.Application.Dto;
using OttWebApp.Application.Query.Movie;

namespace OttWebApp.Controllers;

[Route("api/movies")]
[ApiController]
[Produces("application/json")]
public class MovieController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovieController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public Task<MovieDto?> Index(int id, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetMovie
        {
            Id = id
        }, cancellationToken);
    }

    [HttpGet("popular")]
    public Task<PaginatedListDto<MovieDto>?> Index(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetPopularMovies(), cancellationToken);
    }

    [HttpGet("latest")]
    public Task<MovieDto?> Latest(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetLatestMovie(), cancellationToken);
    }
}