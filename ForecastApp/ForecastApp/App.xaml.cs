using ForecastApp.Services;
using ForecastApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForecastApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitNavigation();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        private Task InitNavigation()
        {
            var navigationService = Resolver.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
