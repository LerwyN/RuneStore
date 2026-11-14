using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(
                userName: model.UserName,
                password: model.Password,
                isPersistent: false, // kullanıcı tarayıcıdan tamamen kapattıktan sonra giriş yapmış kalacak mı demek
                lockoutOnFailure: false // şifre denemelerinde hesabın otomatik kilitlenip kilitlenmeyeceğini belirler
            );
            if(result.Succeeded)
            {
                TempData["SuccessTitle"] = "Başarılı";
                TempData["SuccessMessage"] = "Giriş yapıldı.";
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessTitle"] = "Başarılı";
            TempData["SuccessMessage"] = "Çıkış yapıldı.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email 
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                TempData["SuccessTitle"] = "Başarılı";
                TempData["SuccessMessage"] = "Hesabın oluşturuldu.";
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


    }
}

