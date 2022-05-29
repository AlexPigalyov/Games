using MediatR;

namespace Games.Application.Interfaces.Queries;

public abstract class Query<TResult> : IRequest<TResult> where TResult : new()
{
}