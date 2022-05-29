using Games.Application.Games.Command;
using Games.Application.Interfaces.Commands;
using Games.Domain.Models;
using Games.Infrastructure.Interfaces.CommandHandlers;
using MediatR;
using OneOf;

namespace Games.Infrastructure.Game.CommandHandlers;

public class AddGameCommandHandler : CommandHandlerBase,
    IRequestHandler<AddGameCommand, OneOf<string, CommandError<AddGameCommand>>>
{
    private readonly GamesDbContext _context;

    public AddGameCommandHandler(GamesDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<string, CommandError<AddGameCommand>>> Handle(AddGameCommand request,
        CancellationToken cancellationToken)
    {
        return await TryAsync(request, async com =>
        {
            var gameGuid = Guid.NewGuid();

            await _context.Genres.AddRangeAsync(com.GenresTitle.Select(x => new Genre
            {
                GameId = gameGuid,
                Title = x
            }));

            await _context.Games.AddAsync(new Domain.Models.Game
            {
                Title = com.Title,
                CreatedAt = DateTime.Now,
                Developer = com.Developer,
                Studio = com.Studio,
                Id = gameGuid
            });

            await _context.SaveChangesAsync();

            return "Success";
        });
    }
}