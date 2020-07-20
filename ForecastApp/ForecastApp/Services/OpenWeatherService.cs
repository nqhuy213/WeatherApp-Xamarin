using ForecastApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApp.Services
{
    public class OpenWeatherService : IWeatherService
    {
        public async Task<Forecast> GetForecast(double latitude, double longtitude)
        {
            var language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var apiKey = "bd8709b8c8a3cf22db8fc53027289d96";
            var uri = $"https://api.openweathermap.org/data/2.5/forecast?" +
                $"lat={latitude}&lon={longtitude}" +
                $"&units=metrics&lang={language}&appid={apiKey}";
            var client = new HttpClient();
            var result = await client.GetStringAsync(uri);
            var data = JsonConvert.DeserializeObject<WeatherData>(result);
            var forecast = new Forecast()
            {
                City = data.city.name,
                Items = data.list.Select(x => new ForecastItem()
                {
                    DateTime = ToDateTime(x.dt),
                    Temperature = x.main.temp,
                    WindSpeed = x.wind.speed,
                    Description = x.weather.First().description,
                    Icon = $"http://openweathermap.org/img/w/{x.weather.First().icon}.png"
                }).ToList()
            };
            return forecast;
        }
        private DateTime ToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0,
            DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
