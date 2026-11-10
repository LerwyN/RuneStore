namespace RUNE.Models.ReleaseNotes;

public enum ReleaseNoteCategory
{
    New,
    Updated,
    Removed
}

public class ReleaseNote
{
    public int Id { get; set; }

    public string Version { get; set; } = string.Empty;

    public DateOnly ReleaseDate { get; set; }

    public ReleaseNoteCategory Category { get; set; }

    public string Description { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}
