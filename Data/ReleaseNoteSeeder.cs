using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUNE.Models.ReleaseNotes;

namespace RUNE.Data;

public static class ReleaseNoteSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
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
                Description = "Hediye et sistemi eklendi.",
                DisplayOrder = 3
            },
            new()
            {
                Version = "v0.1.1 Beta",
                ReleaseDate = new DateOnly(2026, 10, 29),
                Category = ReleaseNoteCategory.New,
                Description = "Hediye et banner geçiş ekranı eklendi.",
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

        await context.Notes.AddRangeAsync(releaseNotes);
        await context.SaveChangesAsync();
    }
}
