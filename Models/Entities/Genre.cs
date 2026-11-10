using System;

namespace RUNE.Models.Entities;

public class Genre
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public ICollection<Game> Games { get; set; } = [];

}
