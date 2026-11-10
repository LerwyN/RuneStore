using System;

namespace RUNE.Models.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Game> Game { get; set; } = [];
}
