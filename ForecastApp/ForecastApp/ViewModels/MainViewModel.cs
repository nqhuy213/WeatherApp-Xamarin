using ForecastApp.Models;
using ForecastApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ForecastApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        #region Fields
        private string city;
        private ObservableCollection<ForecastGroup> days;
        private readonly IWeatherService weatherService;
        private bool isRefreshing;
        public ICommand Refresh => new Command(async () =>
        {
            await LoadData();
        });
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
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => Set(ref isRefreshing, value);
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
            IsRefreshing = true;
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
            IsRefreshing = false;
        }

        #endregion
    }
}
