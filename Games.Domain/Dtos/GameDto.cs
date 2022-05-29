namespace Games.Domain.Dtos;

public class GameDto
{
    public string Title { get; set; }
    public List<string> GameGenres { get; set; }
    public string Studio { get; set; }
    public string Developer { get; set; }
}