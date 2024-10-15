using Vives.Presentation.Authentication;

namespace VivesBlog.Ui.ConsoleApp.Stores
{
	public class BearerTokenStore : IBearerTokenStore
	{
		private static string _token = string.Empty;

		public string GetToken()
		{
			return _token;
		}

		public void SetToken(string token)
		{
			_token = token;
		}

	}
}
