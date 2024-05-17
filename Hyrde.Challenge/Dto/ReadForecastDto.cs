namespace Hyrde.Challenge.Dto
{
    public class ReadForecastDto
    {
        public string? ForecastDate { get; set; }
        public string? ConditionIcon { get; set; }
        public float MaxTempCelcius { get; set; }
        public float MinTempCelcius { get; set; }
    }
}