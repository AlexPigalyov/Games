using Games.Application.Interfaces.Queries;
using Games.Domain.Dtos;
using OneOf;

namespace Games.Application.Games.Query;

public class ReadGamesQuery : Query<OneOf<List<GameDto>, QueryError<ReadGamesQuery>>>, IQuery
{
}