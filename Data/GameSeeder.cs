using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RUNE.Models;

namespace RUNE.Data;

public static class GameSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var games = new List<Game>
        {
            new()
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
            },
            NewGame("Grand Theft Auto VI", "grand-theft-auto-vi", "Açık dünya aksiyon-macera", "/images/profiles/GrandTheftAutoVI-Profile.png", "/images/banners/GrandTheftAutoVI-Banner.avif", 2),

            NewGame("Red Dead Redemption 2", "red-dead-redemption-2", "Açık dünya aksiyon-macera", "/images/profiles/red-dead-redemption-profile.jpg", "/images/banners/Red-Dead-Redemption2-Banner.jpg", 3),

            NewGame("Cyberpunk 2077", "cyberpunk-2077", "Açık dünya rol yapma", "/images/profiles/cyberpunk-profile.avif", "/images/banners/cyberpunk.jpeg", 4),

            NewGame("Ghost of Tsushima", "ghost-of-tsushima", "Açık dünya aksiyon-macera", "/images/banners/GhostofTsushima-Banner.jpg", "/images/banners/GhostofTsushima-Banner.jpg", 5),

            NewGame("Hogwarts Legacy", "hogwarts-legacy", "Açık dünya aksiyon rol yapma", "/images/profiles/Hogwarts-Legacy-profile.avif", "/images/banners/Hogwarts-Legacy-banner.avif", 6),

            NewGame("Baldur's Gate 3", "baldurs-gate-3", "Fantastik rol yapma", "/images/profiles/baldurs-gate3-profile.avif", "/images/banners/baldurs-gate3-banner.avif", 7),

            NewGame("Counter-Strike 2", "counter-strike-2", "Çevrimiçi taktiksel nişancı", "/images/profiles/Counter-Strike2-profile.jpg", "/images/banners/Counter-Strike-2-banner.jpeg", 8),

            NewGame("Elden Ring", "elden-ring", "Açık dünya aksiyon rol yapma", "/images/profiles/elden-ring-profile.webp", "/images/banners/elden-ring-banner.avif", 9),

            NewGame("Minecraft", "minecraft", "Açık dünya hayatta kalma", "/images/profiles/Minecraft-profile.jpg", "/images/banners/Minecraft-banner.jpg", 10),

            NewGame("The Last of Us Part I", "the-last-of-us-part-i", "Aksiyon-macera", "/images/profiles/the-last-of-us-profile.png", "/images/banners/TheLastOfUs-Banner.jpg", 11),

            NewGame("The Witcher 3: Wild Hunt", "the-witcher-3-wild-hunt", "Açık dünya rol yapma", "/images/banners/TheWitcher3WildHunt-Banner.jpg", "/images/banners/TheWitcher3WildHunt-Banner.jpg", 12),

            NewGame("Resident Evil 4", "resident-evil-4", "Hayatta kalma-korku", "/images/profiles/resident-evile4-profile.avif", "/images/banners/resident-evile4-banner.webp", 13),

            NewGame("Marvel's Spider-Man 2", "marvels-spider-man-2", "Açık dünya aksiyon-macera", "/images/banners/Spider-Man2-Banner.jpg", "/images/banners/Spider-Man2-Banner.jpg", 14),

            NewGame("God of War Ragnarök", "god-of-war-ragnarok", "Aksiyon-macera", "/images/banners/GodOfWarRagnaörk-Banner.jpg", "/images/banners/GodOfWarRagnaörk-Banner.jpg", 15),

            NewGame("Alan Wake 2", "alan-wake-2", "Hayatta kalma-korku", "/images/banners/AlanWake2-Banner.jpg", "/images/banners/AlanWake2-Banner.jpg", 16),

            NewGame("Death Stranding 2: On the Beach", "death-stranding-2", "Aksiyon-macera", "/images/banners/DeathStranding2OntheBeach-Banner.jpg", "/images/banners/DeathStranding2OntheBeach-Banner.jpg", 17),

            NewGame("DOOM: The Dark Ages", "doom-the-dark-ages", "Aksiyon-nişancı", "/images/banners/Doom-Banner.avif", "/images/banners/Doom-Banner.avif", 18),

            NewGame("EA SPORTS FC 27", "ea-sports-fc-27", "Futbol", "/images/profiles/fc-27-profile.avif", "/images/banners/fc-27-banner.avif", 19),

            NewGame("Resident Evil Requiem", "resident-evil-requiem", "Hayatta kalma-korku", "/images/banners/Resident-Evil-Requiem-Banner.avif", "/images/banners/Resident-Evil-Requiem-Banner.avif", 20),
            
        };

        var existingSlugs = (await context.Games
                .Where(game => game.Slug != null)
                .Select(game => game.Slug!)
                .ToListAsync())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingGames = games
            .Where(game => game.Slug != null && existingSlugs.Add(game.Slug))
            .ToList();

        if (missingGames.Count == 0)
        {
            return;
        }

        await context.Games.AddRangeAsync(missingGames);
        await context.SaveChangesAsync();
    }

    private static Game NewGame(string title, string slug, string genre, string coverPath, string bannerPath, int order) => new()
    {
        Title = title,
        Description = $"{genre} oyunu.",
        Price = 0m,
        Slug = slug,
        Genre = genre,
        CoverImagePath = coverPath,
        BannerImagePath = bannerPath,
        IsPublished = true,
        DisplayOrder = order
    };
}
