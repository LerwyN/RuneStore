using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUNE.Data;

namespace RUNE.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GamesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await _applicationDbContext.Games
                .Where(game => game.IsPublished)
                .Include(game => game.Genre)
                .Include(game => game.Tag)
                .FirstOrDefaultAsync(game => game.Id == id);

            if (game is null)
            {
                return NotFound();
            }

            return View(game);
        }

    }
}
