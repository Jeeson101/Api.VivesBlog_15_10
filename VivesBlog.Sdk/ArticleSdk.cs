using System.Net.Http.Json;
using Vives.Services.Model;
using VivesBlog.Dtos.Requests;
using VivesBlog.Dtos.Results;

namespace VivesBlog.Sdk
{
    public class ArticleSdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IList<ArticleResult>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = "Articles";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResult>>();

            if (result is null)
            {
                return new List<ArticleResult>();
            }

            return result;
        }

        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"Articles/{id}";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();

            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"Articles";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();

            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"Articles/{id}";

            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();

            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"Articles/{id}";

            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult>();

            if (result is null)
            {
                return new ServiceResult();
            }

            return result;
        }
    }
}
