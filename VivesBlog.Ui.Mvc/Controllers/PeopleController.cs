using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dtos.Requests;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personSdk.Find();

            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _personSdk.Create(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            var result = await _personSdk.Get(id);

            if (result.Data is null)
            {
                return RedirectToAction("Index");
            }

            var request = new PersonRequest
            {
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                Email = result.Data.Email
            };
            
            return View(request);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm]PersonRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _personSdk.Update(id, request);

            return RedirectToAction("Index");
        }


        [HttpPost("/[controller]/Delete/{id:int?}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personSdk.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
