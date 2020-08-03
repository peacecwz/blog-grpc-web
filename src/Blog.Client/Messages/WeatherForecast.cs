using System;

namespace Blog.Contract
{
    internal partial class WeatherForecast
    {
        public DateTime DateTime => Date.ToDateTime();

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
