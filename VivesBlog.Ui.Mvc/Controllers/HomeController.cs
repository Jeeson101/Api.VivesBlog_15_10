using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesBlog.Sdk;
using VivesBlog.Ui.Mvc.Models;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleSdk _articleSdk;

        public HomeController(ArticleSdk articleSdk)
        {
            _articleSdk = articleSdk;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleSdk.Find();
            return View(articles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
