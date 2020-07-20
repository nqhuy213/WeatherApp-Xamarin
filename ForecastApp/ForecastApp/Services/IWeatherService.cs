using ForecastApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApp.Services
{
    public interface IWeatherService
    {
        Task<Forecast> GetForecast(double latitude, double longtitude);
    }
}
