using Hyrde.Challenge.Services;

namespace Hyrde.Challenge.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureApiHttpClients(this IServiceCollection _services, IConfiguration configuration)
        {
            var baseUrl = configuration.GetValue<string>("WeatherApi:BaseUrl");
            var apiKey = configuration.GetValue<string>("WeatherApi:ApiKey");
            _services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                if (baseUrl != null)
                {
                    client.BaseAddress = new Uri(baseUrl);
                }
                client.DefaultRequestHeaders.Add("api-key-header-name", apiKey);
            });
            return _services;
        }
    }
}