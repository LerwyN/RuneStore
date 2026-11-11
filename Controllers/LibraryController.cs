using Microsoft.AspNetCore.Mvc;

namespace RUNE.Controllers;

public class LibraryController : Controller
{
    public IActionResult Index() => View();
}
