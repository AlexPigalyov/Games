using Games.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Games.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiControllerBase(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException();
    }

    protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
    {
        return await _mediator.Send(query);
    }

    protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
    {
        return await _mediator.Send(command);
    }

    protected ActionResult Single<T, TError>(OneOf<T, TError> dataOneOf) where TError : IError
    {
        return dataOneOf.Match<ActionResult>(data =>
        {
            if (data == null) return NotFound();

            return Ok(data);
        }, error => BadRequest(error.ErrorMessage));
    }

    protected IActionResult Many<T, TError>(OneOf<List<T>, TError> dataOneOf) where TError : IError
    {
        return dataOneOf.Match<IActionResult>(data =>
        {
            if (data == null || data.Count <= 0) return NotFound();

            return Ok(data);
        }, error => BadRequest(error.ErrorMessage));
    }
}