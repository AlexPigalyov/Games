using Games.Application.Interfaces.Commands;
using OneOf;

namespace Games.Application.Games.Command;

public class RemoveGameCommand : Command<OneOf<string, CommandError<RemoveGameCommand>>>, ICommand
{
    public Guid Id { get; set; }
}