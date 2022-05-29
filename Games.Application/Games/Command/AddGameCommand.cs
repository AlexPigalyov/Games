using Games.Application.Interfaces.Commands;
using OneOf;

namespace Games.Application.Games.Command;

public class AddGameCommand : Command<OneOf<string, CommandError<AddGameCommand>>>, ICommand
{
    public string Title { get; set; }
    public string Studio { get; set; }
    public string Developer { get; set; }
    public List<string> GenresTitle { get; set; }
}