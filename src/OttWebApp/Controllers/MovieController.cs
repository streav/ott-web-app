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

    [HttpGet]
    public Task<IEnumerable<MovieDto>?> Index([FromQuery] GetMovies query, CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
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

    [HttpGet("latest-movie")]
    public Task<MovieDto?> LatestMovie(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetLatestMovie(), cancellationToken);
    }

    [HttpGet("latest")]
    public Task<PaginatedListDto<MovieDto>?> Latest(CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetLatestMovies(), cancellationToken);
    }
}