using System.ComponentModel.DataAnnotations;

namespace Games.Domain.Models;

public class Genre : ModelBase
{
    [Key] public Guid Id { get; set; }

    public string Title { get; set; }
    public Game Game { get; set; }
    public Guid GameId { get; set; }
}