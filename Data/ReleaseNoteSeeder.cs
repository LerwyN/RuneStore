using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUNE.Models.ReleaseNotes;

namespace RUNE.Data;

public static class ReleaseNoteSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, string currentVersion)
    {
        
        var releaseNotes = new List<ReleaseNote>
        {
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Cadılar Bayramı etkinliği mağaza banner'ına eklendi.",
                DisplayOrder = 1
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Öne çıkan oyunları sağdaki listeden seçebilirsin.",
                DisplayOrder = 2
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Hediye et özelliğinin ilk arayüz çalışması eklendi.",
                DisplayOrder = 3
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Hediye et düğmesi mağaza tasarımına eklendi.",
                DisplayOrder = 4
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Oyunlar listeli bir şekilde görüntülenebiliyor.",
                DisplayOrder = 5
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Mağaza banner'ı ve oyun listesi mobil ekranlara göre düzenlendi.",
                DisplayOrder = 1
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Mobil cihazlardaki simgeler iyileştirildi.",
                DisplayOrder = 2
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Arama kutusu iyileştirildi.",
                DisplayOrder = 3
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Cadılar Bayramı posteri düzeltildi.",
                DisplayOrder = 4
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Dar mobil ekranlardaki yatay sayfa kayması giderildi.",
                DisplayOrder = 5
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.Updated,
                Description = "Banner yanındaki oyun görselleri hizalandı.",
                DisplayOrder = 6
            }
        };

        var releaseNotes1 = new List<ReleaseNote>
        {
            new()
            {
                Version = "v0.1.10 Beta",
                ReleaseDate = new DateOnly(2026, 10, 31),
                Category = ReleaseNoteCategory.New,
                Description = "Eski yamalara bakabilme özelliği geldi",
                DisplayOrder = 1
            },
            new()
            {
                Version = "v0.1.10 Beta",
                ReleaseDate = new DateOnly(2026, 10, 31),
                Category = ReleaseNoteCategory.Updated,
                Description = "Grand Theft Auto VI banner görseli güncellendi.",
                DisplayOrder = 1
            },
            new()
            {
                Version = "v0.1.10 Beta",
                ReleaseDate = new DateOnly(2026, 10, 31),
                Category = ReleaseNoteCategory.Removed,
                Description = "“Düzeltildi” seçeneği kaldırıldı.",
                DisplayOrder = 1
            },
        };

        var currentReleaseNotes = new List<ReleaseNote>
        {
            new()
            {
                Version = currentVersion,
                ReleaseDate = new DateOnly(2026, 11, 10),
                Category = ReleaseNoteCategory.New,
                Description = "Oyun detay sayfası eklendi.",
                DisplayOrder = 1
            },
            new()
            {
                Version = currentVersion,
                ReleaseDate = new DateOnly(2026, 11, 10),
                Category = ReleaseNoteCategory.Updated,
                Description = "Simge görünümü iyileştirildi.",
                DisplayOrder = 1
            },
            new()
            {
                Version = currentVersion,
                ReleaseDate = new DateOnly(2026, 11, 10),
                Category = ReleaseNoteCategory.Updated,
                Description = "Grand Theft Auto VI görsel yükleme sorunu giderildi.",
                DisplayOrder = 2
            },
            new()
            {
                Version = currentVersion,
                ReleaseDate = new DateOnly(2026, 11, 10),
                Category = ReleaseNoteCategory.Removed,
                Description = "Cadılar Bayramı posteri kaldırıldı.",
                DisplayOrder = 1
            }
        };

        var existingNotes = await context.Notes
            .OrderBy(note => note.Id)
            .ToListAsync();
        var hasChanges = false;
        var correctedDescriptions = new Dictionary<string, string>
        {
            ["Hediye et sistemi eklendi."] = "Hediye et özelliğinin ilk arayüz çalışması eklendi.",
            ["Hediye et banner geçiş ekranı eklendi."] = "Hediye et düğmesi mağaza tasarımına eklendi."
        };

        foreach (var note in existingNotes)
        {
            if (correctedDescriptions.TryGetValue(note.Description, out var correctedDescription))
            {
                note.Description = correctedDescription;
                hasChanges = true;
            }
        }

        var duplicateNotes = existingNotes
            .GroupBy(note => (note.Version, note.ReleaseDate, note.Category, note.Description, note.DisplayOrder))
            .SelectMany(group => group.Skip(1))
            .ToList();

        if (duplicateNotes.Count > 0)
        {
            context.Notes.RemoveRange(duplicateNotes);
            hasChanges = true;
        }

        var duplicateNoteIds = duplicateNotes.Select(note => note.Id).ToHashSet();
        var existingKeys = existingNotes
            .Where(note => !duplicateNoteIds.Contains(note.Id))
            .Select(note => (note.Version, note.ReleaseDate, note.Category, note.Description, note.DisplayOrder))
            .ToHashSet();

        var missingNotes = releaseNotes
            .Concat(releaseNotes1)
            .Concat(currentReleaseNotes)
            .Where(note => existingKeys.Add((note.Version, note.ReleaseDate, note.Category, note.Description, note.DisplayOrder)))
            .ToList();

        if (missingNotes.Count > 0 || hasChanges)
        {
            await context.Notes.AddRangeAsync(missingNotes);
            await context.SaveChangesAsync();
        }
    }
}
