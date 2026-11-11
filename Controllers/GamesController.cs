using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUNE.Data;
using RUNE.Models;

namespace RUNE.Controllers;

public class GamesController : Controller
{
    private readonly ApplicationDbContext _context;

    public GamesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => RedirectToAction("Index", "Home");

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null or <= 0)
        {
            return NotFound();
        }

        var game = await _context.Games
            .AsNoTracking()
            .Include(item => item.Genre)
            .Include(item => item.Tag)
            .FirstOrDefaultAsync(item => item.Id == id.Value && item.IsPublished);

        if (game is null)
        {
            return NotFound();
        }

        return View(game);
    }
}
