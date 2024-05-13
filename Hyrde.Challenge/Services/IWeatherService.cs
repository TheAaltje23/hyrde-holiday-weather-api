using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<Weather>> GetForecast(string query);
        Task<Weather> GetToday(string query);
    }
}