using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Dtos.Requests;
using VivesBlog.Services;

namespace VivesBlog.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController(ArticleService articleService) : ControllerBase
    {
        private readonly ArticleService _articleService = articleService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var result = await _articleService.Find();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _articleService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleRequest request)
        {
            var result = await _articleService.Create(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ArticleRequest request)
        {
            var result = await _articleService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.Delete(id);
            return Ok(result);
        }
    }
}
