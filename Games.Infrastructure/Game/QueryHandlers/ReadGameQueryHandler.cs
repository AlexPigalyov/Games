using AutoMapper;
using Games.Application.Games.Query;
using Games.Application.Interfaces.Queries;
using Games.Domain.Dtos;
using Games.Infrastructure.Interfaces.QueryHandlers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Games.Infrastructure.Game.QueryHandlers;

public class ReadGameQueryHandler : QueryHandlerBase,
    IRequestHandler<ReadGamesQuery, OneOf<List<GameDto>, QueryError<ReadGamesQuery>>>,
    IRequestHandler<ReadGamesByGenreQuery, OneOf<List<GameDto>, QueryError<ReadGamesByGenreQuery>>>
{
    private readonly GamesDbContext _context;
    private readonly IMapper _mapper;

    public ReadGameQueryHandler(
        GamesDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OneOf<List<GameDto>, QueryError<ReadGamesByGenreQuery>>> Handle(ReadGamesByGenreQuery request,
        CancellationToken cancellationToken)
    {
        return await TryAsync(request, async com =>
        {
            var games = _context.Games
                .AsNoTracking()
                .Include(x => x.GameGenres)
                .Where(x => x.GameGenres.Any(g => g.Title == com.GenreTitle));

            return await games.Select(x => _mapper.Map<GameDto>(x))
                .ToListAsync();
        });
    }

    public async Task<OneOf<List<GameDto>, QueryError<ReadGamesQuery>>> Handle(ReadGamesQuery request,
        CancellationToken cancellationToken)
    {
        return await TryAsync(request, async com =>
        {
            var games = _context.Games
                .AsNoTracking()
                .Include(x => x.GameGenres);

            return await games.Select(x => _mapper.Map<GameDto>(x))
                .ToListAsync();
        });
    }
}