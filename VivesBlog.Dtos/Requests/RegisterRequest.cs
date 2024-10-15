using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Dtos.Requests
{
	public class RegisterRequest
	{
		[Required]
		[EmailAddress]
		public required string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public required string Password { get; set; }
	}
}
