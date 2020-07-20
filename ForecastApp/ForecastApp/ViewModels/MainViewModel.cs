using ForecastApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ForecastApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private readonly IWeatherService weatherService;
        public MainViewModel(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        public async Task LoadData()
        {
            var location = await Geolocation.GetLocationAsync();
            var forecast = await weatherService.GetForecast(location.Latitude, location.Longitude);
        }
    }
}
