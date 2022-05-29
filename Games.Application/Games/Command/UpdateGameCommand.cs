using Games.Application.Interfaces.Commands;
using OneOf;

namespace Games.Application.Games.Command;

public class UpdateGameCommand : Command<OneOf<string, CommandError<UpdateGameCommand>>>, ICommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> GameGenres { get; set; }
    public string Studio { get; set; }
    public string Developer { get; set; }
}