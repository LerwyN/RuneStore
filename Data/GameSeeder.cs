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
        const string gtaViSlug = "grand-theft-auto-vi";
        const string gtaViBannerPath = "/images/banners/GrandTheftAutoVI-banner.jpg";

        var games = new List<Game>
        {
            new()
            {
                Title = "Grand Theft Auto V",
                Description = "Los Santos'ta geçen açık dünya aksiyon-macera oyunu.",
                Price = 1.049m,
                Slug = "grand-theft-auto-v",
                Developer = "Rockstar North",
                Publisher = "Rockstar Games",
                ReleaseDate = new DateTime(2013, 9, 17, 0, 0, 0, DateTimeKind.Utc),
                CoverImagePath = "/images/profiles/grand-theft-autoV-profile.jpg",
                BannerImagePath = "/images/banners/grand-theft-autoV-banner.jpg",
                IsPublished = true,
                DisplayOrder = 1
            },
            NewGame("Grand Theft Auto VI", "grand-theft-auto-vi", "Açık dünya aksiyon-macera", "/images/profiles/GrandTheftAutoVI-Profile.png", "/images/banners/GrandTheftAutoVI-banner.jpg", 2),

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

        var gameDetails = new Dictionary<string, (string Developer, string Publisher, DateTime? ReleaseDate)>(StringComparer.OrdinalIgnoreCase)
        {
            ["grand-theft-auto-v"] = ("Rockstar North", "Rockstar Games", UtcDate(2013, 9, 17)),
            ["grand-theft-auto-vi"] = ("Rockstar North", "Rockstar Games", UtcDate(2026, 11, 19)),
            ["red-dead-redemption-2"] = ("Rockstar Studios", "Rockstar Games", UtcDate(2018, 10, 26)),
            ["cyberpunk-2077"] = ("CD Projekt Red", "CD Projekt", UtcDate(2020, 12, 10)),
            ["ghost-of-tsushima"] = ("Sucker Punch Productions", "Sony Interactive Entertainment", UtcDate(2020, 7, 17)),
            ["hogwarts-legacy"] = ("Avalanche Software", "Warner Bros. Games", UtcDate(2023, 2, 10)),
            ["baldurs-gate-3"] = ("Larian Studios", "Larian Studios", UtcDate(2023, 8, 3)),
            ["counter-strike-2"] = ("Valve", "Valve", UtcDate(2023, 9, 27)),
            ["elden-ring"] = ("FromSoftware", "Bandai Namco Entertainment", UtcDate(2022, 2, 25)),
            ["minecraft"] = ("Mojang Studios", "Xbox Game Studios", UtcDate(2011, 11, 18)),
            ["the-last-of-us-part-i"] = ("Naughty Dog", "Sony Interactive Entertainment", UtcDate(2022, 9, 2)),
            ["the-witcher-3-wild-hunt"] = ("CD Projekt Red", "CD Projekt", UtcDate(2015, 5, 19)),
            ["resident-evil-4"] = ("Capcom", "Capcom", UtcDate(2023, 3, 24)),
            ["marvels-spider-man-2"] = ("Insomniac Games", "Sony Interactive Entertainment", UtcDate(2023, 10, 20)),
            ["god-of-war-ragnarok"] = ("Santa Monica Studio", "Sony Interactive Entertainment", UtcDate(2022, 11, 9)),
            ["alan-wake-2"] = ("Remedy Entertainment", "Epic Games Publishing", UtcDate(2023, 10, 27)),
            ["death-stranding-2"] = ("Kojima Productions", "Sony Interactive Entertainment", UtcDate(2025, 6, 26)),
            ["doom-the-dark-ages"] = ("id Software", "Bethesda Softworks", UtcDate(2025, 5, 15)),
            ["ea-sports-fc-27"] = ("EA Vancouver / EA Romania", "Electronic Arts", UtcDate(2026, 9, 25)),
            ["resident-evil-requiem"] = ("Capcom", "Capcom", UtcDate(2026, 2, 27))
        };

        foreach (var game in games)
        {
            if (game.Slug is not null && gameDetails.TryGetValue(game.Slug, out var details))
            {
                game.Developer = details.Developer;
                game.Publisher = details.Publisher;
                game.ReleaseDate = details.ReleaseDate;
            }
        }

        var hasChanges = false;
        var existingGames = await context.Games
            .Where(game => game.Slug != null)
            .ToListAsync();
        var existingGamesBySlug = existingGames
            .GroupBy(game => game.Slug!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

        foreach (var seededGame in games)
        {
            if (seededGame.Slug is null || !existingGamesBySlug.TryGetValue(seededGame.Slug, out var existingGame))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(existingGame.Developer) && !string.IsNullOrWhiteSpace(seededGame.Developer))
            {
                existingGame.Developer = seededGame.Developer;
                hasChanges = true;
            }

            if (string.IsNullOrWhiteSpace(existingGame.Publisher) && !string.IsNullOrWhiteSpace(seededGame.Publisher))
            {
                existingGame.Publisher = seededGame.Publisher;
                hasChanges = true;
            }

            if (existingGame.ReleaseDate is null && seededGame.ReleaseDate is not null)
            {
                existingGame.ReleaseDate = seededGame.ReleaseDate;
                hasChanges = true;
            }

            if (seededGame.Slug == gtaViSlug && existingGame.BannerImagePath != gtaViBannerPath)
            {
                existingGame.BannerImagePath = gtaViBannerPath;
                hasChanges = true;
            }
        }

        var existingSlugs = existingGamesBySlug.Keys.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingGames = games
            .Where(game => game.Slug != null && existingSlugs.Add(game.Slug))
            .ToList();

        if (missingGames.Count > 0)
        {
            await context.Games.AddRangeAsync(missingGames);
            hasChanges = true;
        }

        if (hasChanges)
        {
            await context.SaveChangesAsync();
        }
    }

    private static Game NewGame(string title, string slug, string genre, string coverPath, string bannerPath, int order) => new()
    {
        Title = title,
        Description = $"{genre} oyunu.",
        Price = 0m,
        Slug = slug,
        CoverImagePath = coverPath,
        BannerImagePath = bannerPath,
        IsPublished = true,
        DisplayOrder = order
    };

    private static DateTime UtcDate(int year, int month, int day) =>
        new(year, month, day, 0, 0, 0, DateTimeKind.Utc);
}
