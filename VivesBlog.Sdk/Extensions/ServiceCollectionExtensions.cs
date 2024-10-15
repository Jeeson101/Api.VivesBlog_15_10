using Microsoft.Extensions.DependencyInjection;
using VivesBlog.Sdk.Handlers;

namespace VivesBlog.Sdk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddScoped<AuthorizationHandler>();

            services.AddHttpClient("VivesBlogApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(apiUrl);
            }).AddHttpMessageHandler<AuthorizationHandler>();

			services.AddScoped<IdentitySdk>();
            services.AddScoped<ArticleSdk>();
            services.AddScoped<PersonSdk>();

            return services;
        }
    }
}
