using Games.Application.Games.Command;
using Games.Application.Interfaces.Commands;
using Games.Infrastructure.Interfaces.CommandHandlers;
using MediatR;
using OneOf;

namespace Games.Infrastructure.Game.CommandHandlers;

public class RemoveGameCommandHandler : CommandHandlerBase,
    IRequestHandler<RemoveGameCommand, OneOf<string, CommandError<RemoveGameCommand>>>
{
    private readonly GamesDbContext _context;

    public RemoveGameCommandHandler(GamesDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<string, CommandError<RemoveGameCommand>>> Handle(RemoveGameCommand request,
        CancellationToken cancellationToken)
    {
        return await TryAsync(request, async com =>
        {
            var game = await _context.Games.FindAsync(com.Id);

            _context.Games.Remove(game);
            _context.Genres.RemoveRange(game.GameGenres);

            await _context.SaveChangesAsync();

            return "Success";
        });
    }
}