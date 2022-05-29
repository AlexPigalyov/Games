namespace Games.Application.Interfaces.Commands;

public class CommandError<T> : IError where T : ICommand, new()
{
    public string ErrorMessage { get; set; }
}