using MediatR;
using Microsoft.AspNetCore.Mvc;
using OttWebApp.Application.Dto;
using OttWebApp.Application.Query.Person;

namespace OttWebApp.Controllers;

[Route("api/person")]
[ApiController]
[Produces("application/json")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public Task<PersonDto?> Index(int id, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetPerson { Id = id }, cancellationToken);
    }
}