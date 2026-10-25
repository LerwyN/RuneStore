using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUNE.Models;

namespace RUNE.Data;

public static class GameSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var gtaExists = await context.Games
            .AnyAsync(game => game.Slug == "grand-theft-auto-v");

        if (gtaExists)
        {
            return;
        }

        var game = new Game
        {
            Title = "Grand Theft Auto V",
            Description = "Los Santos'ta geçen açık dünya aksiyon-macera oyunu.",
            Price = 799.00m,
            Slug = "grand-theft-auto-v",
            Genre = "Aksiyon-Macera",
            Developer = "Rockstar North",
            Publisher = "Rockstar Games",
            ReleaseDate = new DateTime(2013, 9, 17, 0, 0, 0, DateTimeKind.Utc),
            CoverImagePath = "/images/profiles/grand-theft-autoV-profile.jpg",
            BannerImagePath = "/images/banners/grand-theft-autoV-banner.jpg",
            IsPublished = true,
            DisplayOrder = 1
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
    }
}