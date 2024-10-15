using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vives.Presentation.Authentication;
using VivesBlog.Dtos.Requests;
using VivesBlog.Sdk;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class AccountController : Controller
    {
		private readonly IdentitySdk _identitySdk;
		private readonly IBearerTokenStore _tokenStore;

		public AccountController(IdentitySdk identitySdk, IBearerTokenStore tokenStore)
		{
			_identitySdk = identitySdk;
			_tokenStore = tokenStore;
		}

		[HttpGet]
		public async Task<IActionResult> SignIn([FromQuery] string? returnUrl)
		{
			returnUrl ??= "/";

			await HttpContext.SignOutAsync();
			_tokenStore.SetToken(string.Empty);

			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn([FromForm] SignInModel model, [FromQuery] string? returnUrl)
		{
			returnUrl ??= "/";
			ViewBag.ReturnUrl = returnUrl;

			if (!ModelState.IsValid)
			{
				return View();
			}

			var request = new SignInRequest
			{
				Username = model.Email,
				Password = model.Password
			};
			var result = await _identitySdk.SignIn(request);


			if (!result.IsSuccess || result.Token is null)
			{
				ModelState.AddModelError("", "Login failed.");
				return View();
			}

			await InternalSignIn(model.Email, model.RememberMe);
			_tokenStore.SetToken(result.Token);

			return LocalRedirect(returnUrl);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout([FromQuery] string? returnUrl)
		{
			returnUrl ??= "/";

			await HttpContext.SignOutAsync();
			_tokenStore.SetToken(string.Empty);

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
		public async Task<IActionResult> Register([FromForm] RegisterModel model, [FromQuery] string? returnUrl)
		{
			returnUrl ??= "/";
			ViewBag.ReturnUrl = returnUrl;

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var request = new RegisterRequest
			{
				Username = model.Email,
				Password = model.Password
			};

			var result = await _identitySdk.Register(request);

			if (!result.IsSuccess || result.Token is null)
			{
				foreach (var error in result.Messages)
				{
					ModelState.AddModelError("", error.Message);
				}
				return View(model);
			}

			await InternalSignIn(model.Email);
			_tokenStore.SetToken(result.Token);

			return LocalRedirect(returnUrl);
		}

		private async Task InternalSignIn(string email, bool isPersistent = false)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, email)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
			{
				IsPersistent = isPersistent
			});
		}
	}
}
