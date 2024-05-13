using Hyrde.Challenge.Models;
using Newtonsoft.Json;

namespace Hyrde.Challenge.Services
{
    public class WeatherService : IWeatherService
    {
        // You can use this HttpClient as a starting point to interface with the API of your choice.
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public WeatherService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WeatherApi:BaseUrl"] ?? throw new ArgumentException("WeatherApi:BaseUrl configuration is missing or null");
            _apiKey = configuration["WeatherApi:ApiKey"] ?? throw new ArgumentException("WeatherApi:ApiKey configuration is missing or null");
        }

        public async Task<IEnumerable<Weather>> GetForecast(string query)
        {
            // PLACEHOLDER
            string url = $"{_baseUrl}/current.json?key={_apiKey}&q={query}";
            HttpResponseMessage response = await _client.GetAsync(url);

            throw new NotImplementedException("GetForecast method is not implemented yet.");

        }

        public async Task<Weather> GetToday(string query)
        {
            string url = $"{_baseUrl}/current.json?key={_apiKey}&q={query}";

            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);

                Weather weather = new Weather
                {
                    TempCelcius = (float)data.current.temp_c,
                    ConditionText = data.current.condition.text,
                    ConditionIcon = data.current.condition.icon,
                    WindKph = (float)data.current.wind_kph,
                    PrecipitationMm = (float)data.current.precip_mm,
                    Humidity = (int)data.current.humidity,
                    City = data.location.name,
                    Country = data.location.country,
                    DateTime = data.location.localtime
                };

                return weather;
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve weather data.");
            }
        }
    }
}