using MediatR;

namespace Games.Application.Interfaces.Commands;

public class Command<T> : IRequest<T> where T : new()
{
}