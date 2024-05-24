using System.Globalization;
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
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient client, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _client = client;
            _baseUrl = configuration["WeatherApi:BaseUrl"] ?? throw new ArgumentException("WeatherApi:BaseUrl configuration is missing or null");
            _apiKey = configuration["WeatherApi:ApiKey"] ?? throw new ArgumentException("WeatherApi:ApiKey configuration is missing or null");
            _logger = logger;
        }

        public async Task<Weather?> GetToday(string query, string unit)
        {
            string url = $"{_baseUrl}/current.json?key={_apiKey}&q={query}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic? data = JsonConvert.DeserializeObject(json);

                    Weather currentWeather = new Weather
                    {
                        TempCelcius = (float)data?.current.temp_c,
                        TempFahrenheit = (float)data?.current.temp_f,
                        ConditionText = data?.current.condition.text,
                        ConditionIcon = data?.current.condition.icon,
                        WindKph = (float)data?.current.wind_kph,
                        PrecipitationMm = (float)data?.current.precip_mm,
                        Humidity = (int)data?.current.humidity,
                        City = data?.location.name,
                        Country = data?.location.country,
                        Region = data?.location.region,
                        WindDir = data?.current.wind_dir
                    };
                    return currentWeather;
                }
                else
                {
                    _logger.LogError("Failed to retrieve current weather data from API. Status code: {StatusCode}", response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while retrieving current weather data");
                return null;
            }
        }

        public async Task<IEnumerable<Weather>?> GetForecast(string query, string unit)
        {
            string url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={query}&days=5";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic? data = JsonConvert.DeserializeObject(json);

                    List<Weather> forecast = new List<Weather>();

                    if (data != null)
                    {
                        foreach (var f in data.forecast.forecastday)
                        {
                            Weather weather = new Weather
                            {
                                ForecastDate = ConvertDate((string)f.date),
                                ConditionIcon = f.day.condition.icon,
                                MaxTempCelcius = f.day.maxtemp_c,
                                MinTempCelcius = f.day.mintemp_c,
                                MaxTempFahrenheit = f.day.maxtemp_f,
                                MinTempFahrenheit = f.day.mintemp_f

                            };
                            forecast.Add(weather);
                        }
                    }
                    return forecast;
                }
                else
                {
                    _logger.LogError("Failed to retrieve forecast weather data from API. Status code: {StatusCode}", response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while retrieving forecast weather data");
                return null;
            }
        }

        // Convert (yyyy-mm-dd) to day of the week: Mon, Tue, Wed, etc.
        public string ConvertDate(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dayOfWeek = dateTime.ToString("ddd", new CultureInfo("en-US"));
            return dayOfWeek;
        }
    }
}