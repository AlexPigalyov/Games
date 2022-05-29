using Games.Application.Interfaces.Queries;
using Games.Domain.Dtos;
using OneOf;

namespace Games.Application.Games.Query;

public class ReadGamesByGenreQuery : Query<OneOf<List<GameDto>, QueryError<ReadGamesByGenreQuery>>>, IQuery
{
    public string GenreTitle { get; set; }
}