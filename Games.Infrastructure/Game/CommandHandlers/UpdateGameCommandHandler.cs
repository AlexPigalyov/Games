using Games.Application.Games.Command;
using Games.Application.Interfaces.Commands;
using Games.Domain.Models;
using Games.Infrastructure.Interfaces.CommandHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Games.Infrastructure.Game.CommandHandlers;

public class UpdateGameCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateGameCommand, OneOf<string, CommandError<UpdateGameCommand>>>
{
    private readonly GamesDbContext _context;

    public UpdateGameCommandHandler(GamesDbContext context)
    {
        _context = context;
    }

    public async Task<OneOf<string, CommandError<UpdateGameCommand>>> Handle(UpdateGameCommand request,
        CancellationToken cancellationToken)
    {
        return await TryAsync(request, async com =>
        {
            var entity = new Domain.Models.Game
            {
                Title = com.Title,
                Developer = com.Developer,
                Studio = com.Studio,
                UpdatedAt = DateTime.Now
            };

            _context.Games.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            _context.Genres.RemoveRange(_context.Genres
                .Where(x => x.GameId == com.Id));

            await _context.Genres.AddRangeAsync(com.GameGenres.Select(x => new Genre
            {
                GameId = com.Id,
                Title = x
            }));

            await _context.SaveChangesAsync();

            return "Success";
        });
    }
}