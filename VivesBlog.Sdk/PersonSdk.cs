using System.Net.Http.Json;
using Vives.Services.Model;
using VivesBlog.Dtos.Requests;
using VivesBlog.Dtos.Results;

namespace VivesBlog.Sdk
{
    public class PersonSdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IList<PersonResult>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = "People";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();

            if (result is null)
            {
                return new List<PersonResult>();
            }

            return result;
        }

        public async Task<ServiceResult<PersonResult>> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"People/{id}";

            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }

        public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"People";

            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }

        public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"People/{id}";

            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();

            if (result is null)
            {
                return new ServiceResult<PersonResult>();
            }

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");
            var route = $"People/{id}";

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
