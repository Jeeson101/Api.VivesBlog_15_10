using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dtos.Requests;
using VivesBlog.Services;

namespace VivesBlog.Api.Controllers
{
	[Authorize]
	[Route("[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {
        private readonly PersonService _personService = personService;

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var result = await _personService.Find();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _personService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            var result = await _personService.Create(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PersonRequest request)
        {
            var result = await _personService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.Delete(id);
            return Ok(result);
        }
    }
}
