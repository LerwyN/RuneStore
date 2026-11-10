using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

    }
}
