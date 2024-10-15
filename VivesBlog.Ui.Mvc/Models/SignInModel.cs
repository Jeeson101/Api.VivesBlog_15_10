using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Ui.Mvc.Models
{
    public class SignInModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
