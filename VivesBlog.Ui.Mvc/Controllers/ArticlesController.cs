using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dtos.Requests;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ArticleSdk _articleSdk;
        private readonly PersonSdk _personSdk;

        public ArticlesController(
            ArticleSdk articleSdk,
            PersonSdk personSdk)
        {
            _articleSdk = articleSdk;
            _personSdk = personSdk;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _articleSdk.Find();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return await CreateEditView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return await CreateEditView(request);
            }

            await _articleSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _articleSdk.Get(id);

            if (result.Data is null)
            {
                return RedirectToAction("Index");
            }

            var request = new ArticleRequest
            {
                Title = result.Data.Title,
                Description = result.Data.Description,
                Content = result.Data.Content,
                AuthorId = result.Data.AuthorId
            };

            return await CreateEditView(request);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] ArticleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return await CreateEditView(request);
            }

            await _articleSdk.Update(id, request);

            return RedirectToAction("Index");
        }

        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleSdk.Delete(id);

            return RedirectToAction("Index");
        }


        private async Task<IActionResult> CreateEditView(ArticleRequest? request = null)
        {
            var authors = await _personSdk.Find();
            ViewBag.Authors = authors;

            return View(request);
        }
    }
}
