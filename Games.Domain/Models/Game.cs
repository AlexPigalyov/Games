using System.ComponentModel.DataAnnotations;

namespace Games.Domain.Models;

public class Game : ModelBase
{
    [Key] public Guid Id { get; set; }

    public string Title { get; set; }
    public ICollection<Genre> GameGenres { get; set; }
    public string Studio { get; set; }
    public string Developer { get; set; }
}