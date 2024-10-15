using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Core;
using VivesBlog.Dtos.Requests;
using VivesBlog.Dtos.Results;
using VivesBlog.Model;
using VivesBlog.Services.Extensions;

namespace VivesBlog.Services
{
    public class ArticleService
    {
        private readonly VivesBlogDbContext _dbContext;

        public ArticleService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<ArticleResult>> Find()
        {
            return await _dbContext.Articles
                .Include(a => a.Author)
                .Project()
                .ToListAsync();
        }

        //Get (by id)
        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var article = await _dbContext.Articles
                .Include(a => a.Author)
                .Project()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (article is null)
            {
                return new ServiceResult<ArticleResult>().NotFound(nameof(Article));
            }

            return new ServiceResult<ArticleResult>(article);
        }

        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var article = new Article
            {
                Title = request.Title,
                Content = request.Content,
                Description = request.Description,
                AuthorId = request.AuthorId,
                PublishedDate = DateTime.UtcNow
            };

            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();

            return await Get(article.Id);
        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var article = await _dbContext.Articles
                .FirstOrDefaultAsync(p => p.Id == id);

            if (article is null)
            {
                return new ServiceResult<ArticleResult>().NotFound(nameof(Article));
            }

            article.Title = request.Title;
            article.Description = request.Description;
            article.Content = request.Content;
            article.AuthorId = request.AuthorId;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }

        //Delete
        public async Task<ServiceResult> Delete(int id)
        {
            var article = await _dbContext.Articles
                .FirstOrDefaultAsync(p => p.Id == id);

            if (article is null)
            {
                return new ServiceResult().NotFound(nameof(Article));
            }

            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();

            return new ServiceResult();
        }

    }
}
