using Vives.Services.Model;

namespace VivesBlog.Dtos.Results
{
	public class AuthenticationResult : ServiceResult
	{
		public string? Token { get; set; }

	}
}
