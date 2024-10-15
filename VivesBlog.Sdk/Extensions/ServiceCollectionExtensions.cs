using Microsoft.Extensions.DependencyInjection;

namespace VivesBlog.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddHttpClient("VivesBlogApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(apiUrl);
            });

            services.AddScoped<ArticleSdk>();
            services.AddScoped<PersonSdk>();

            return services;
        }
    }
}
