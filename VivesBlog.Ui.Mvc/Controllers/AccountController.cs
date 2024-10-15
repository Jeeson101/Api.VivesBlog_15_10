using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn([FromQuery] string? returnUrl)
        {
            returnUrl ??= "/";

            await _signInManager.SignOutAsync();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromForm]SignInModel model, [FromQuery] string? returnUrl)
        {
            returnUrl ??= "/";
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login failed.");
                return View();
            }

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout([FromQuery] string? returnUrl)
        {
            returnUrl ??= "/";

            await _signInManager.SignOutAsync();

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register([FromQuery] string? returnUrl)
        {
            returnUrl ??= "/";

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm]RegisterModel model, [FromQuery]string? returnUrl)
        {
            returnUrl ??= "/";
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser(model.Email);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);

            return LocalRedirect(returnUrl);
        }
    }
}
