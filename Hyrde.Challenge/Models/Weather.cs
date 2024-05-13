using System.Xml.Linq;

namespace Hyrde.Challenge.Models
{
    public class Weather
    {
        public float TempCelcius { get; set; }
        public string? ConditionText { get; set; }
        public string? ConditionIcon { get; set; }
        public float WindKph { get; set; }
        public float PrecipitationMm { get; set; }
        public int Humidity { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public DateTime DateTime { get; set; }
    }
}
