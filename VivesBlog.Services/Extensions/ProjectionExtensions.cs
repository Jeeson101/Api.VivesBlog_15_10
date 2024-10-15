using VivesBlog.Dtos.Results;
using VivesBlog.Model;

namespace VivesBlog.Services.Extensions
{
    public static class ProjectionExtensions
    {
        public static IQueryable<ArticleResult> Project(this IQueryable<Article> query)
        {
            return query.Select(a => new ArticleResult
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Content = a.Content,
                PublishedDate = a.PublishedDate,
                AuthorId = a.AuthorId,
                AuthorFirstName = a.Author != null ? a.Author.FirstName : null,
                AuthorLastName = a.Author != null ? a.Author.LastName : null
            });
        }

        public static IQueryable<PersonResult> Project(this IQueryable<Person> query)
        {
            return query.Select(a => new PersonResult
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                NumberOfArticles = a.Articles.Count
            });
        }
    }
}
