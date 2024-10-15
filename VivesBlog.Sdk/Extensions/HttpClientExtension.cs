using System.Net.Http.Headers;

namespace VivesBlog.Sdk.Extensions
{
	public static class HttpClientExtension
	{
		public static HttpClient AddAuthorization(this HttpClient client, string token)
		{
			client.DefaultRequestHeaders.AddAuthorization(token);
			return client;
		}
		public static HttpRequestHeaders AddAuthorization(this HttpRequestHeaders headers, string token)
		{
			if (headers.Contains("Authorization"))
			{
				headers.Remove("Authorization");
			}

			headers.Add("Authorization", $"Bearer {token}");

			return headers;
		}
	}
}
