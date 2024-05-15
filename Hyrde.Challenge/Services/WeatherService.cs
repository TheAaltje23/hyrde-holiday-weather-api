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

        public async Task<Weather> GetToday(string query)
        {
            string url = $"{_baseUrl}/current.json?key={_apiKey}&q={query}";

            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                dynamic? data = JsonConvert.DeserializeObject(json);

                Weather weather = new Weather
                {
                    TempCelcius = (float)data?.current.temp_c,
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
                return weather;
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve weather data.");
            }
        }

        public async Task<IEnumerable<Weather>> GetForecast(string query)
        {
            string url = $"{_baseUrl}/forecast.json?key={_apiKey}&q={query}&days=5";

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
                        };
                        forecast.Add(weather);
                    }
                }
                return forecast;
            }
            else
            {
                throw new HttpRequestException("Failed to retrieve weather data.");
            }
        }

        public string ConvertDate(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string dayOfWeek = dateTime.ToString("dddd");
            return dayOfWeek;
        }
    }
}