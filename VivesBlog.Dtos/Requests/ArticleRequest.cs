using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dtos.Requests
{
    public class ArticleRequest
    {

        [Required]
        public required string Title { get; set; }

        public int? AuthorId { get; set; }

        [Required]
        public required string Description { get; set; }
        [Required]
        public required string Content { get; set; }
    }
}
