using ForecastApp.Models;
using ForecastApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ForecastApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        #region Fields
        private string city;
        private ObservableCollection<ForecastGroup> days;
        private readonly IWeatherService weatherService;
        #endregion

        #region Properties
        public string City {
            get => city;
            set => Set(ref city, value);
        }

        public ObservableCollection<ForecastGroup> Days {
            get => days;
            set => Set(ref days, value);
        }
        #endregion

        #region Constructors
        public MainViewModel(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        #endregion

        #region Methods
        public async Task LoadData()
        {
            var location = await Geolocation.GetLocationAsync();
            var forecast = await weatherService.GetForecast(location.Latitude, location.Longitude);
            var itemGroups = new List<ForecastGroup>();
            foreach (var item in forecast.Items)
            {
                if (!itemGroups.Any())
                {
                    itemGroups.Add(new ForecastGroup(
                    new List<ForecastItem>() { item })
                    { Date = item.DateTime.Date });
                    continue;
                }
                var group = itemGroups.SingleOrDefault(x => x.Date ==
                item.DateTime.Date);
                if (group == null)
                {
                    itemGroups.Add(new ForecastGroup(
                    new List<ForecastItem>() { item })
                    { Date = item.DateTime.Date });
                    continue;
                }
                group.Items.Add(item);
            }
            Days = new ObservableCollection<ForecastGroup>(itemGroups);
            City = forecast.City;
        }

        #endregion
    }
}
