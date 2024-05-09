using Hyrde.Challenge.Controllers;
using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<Weather>> GetForecast();
        Task<Weather> Today();
    }
}
