namespace Games.Application.Interfaces.Queries;

public class QueryError<T> : IError where T : IQuery, new()
{
    public string ErrorMessage { get; set; }
}