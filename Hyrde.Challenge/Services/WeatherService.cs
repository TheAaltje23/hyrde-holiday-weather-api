using Hyrde.Challenge.Models;
using Newtonsoft.Json;

namespace Hyrde.Challenge.Services
{
    public class WeatherService : IWeatherService
    {
        // You can use this HttpClient as a starting point to interface with the API of your choice.
        private readonly HttpClient _client;

        public WeatherService(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get the forecast of the oncoming few days
        /// </summary>
        public async Task<IEnumerable<Weather>> GetForecast()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get today's weather
        /// </summary>
        public async Task<Weather> Today()
        {
            throw new NotImplementedException();
        }
    }
}
