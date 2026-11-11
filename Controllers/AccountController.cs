using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RUNE.Data;
using RUNE.Models;
using RUNE.Models.ViewModels.Account;

namespace RUNE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }



    }
}
