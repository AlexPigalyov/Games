using Games.Application.Games.Command;
using Games.Application.Games.Query;
using Games.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Games.API.Controllers;

public class GamesController : ApiControllerBase
{
    public GamesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GameDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetGames([FromQuery] ReadGamesByGenreQuery query)
    {
        return Many(await QueryAsync(query));
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<GameDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Insert([FromBody] AddGameCommand command)
    {
        return Single(await CommandAsync(command));
    }
}