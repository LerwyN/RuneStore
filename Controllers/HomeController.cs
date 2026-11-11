using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUNE.Models;
using RUNE.Data;

namespace RUNE.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _context.Games
            .AsNoTracking()
            .Where(game => game.IsPublished)
            .OrderBy(game => game.DisplayOrder)
            .ToListAsync();

        ViewData["ReleaseNotes"] = await _context.Notes
            .AsNoTracking()
            .OrderByDescending(note => note.ReleaseDate)
            .ThenBy(note => note.DisplayOrder)
            .ToListAsync();

        return View(games);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
