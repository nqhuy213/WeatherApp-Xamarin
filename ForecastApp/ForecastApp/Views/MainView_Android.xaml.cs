using ForecastApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ForecastApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView_Android : ContentPage
    {
        public MainView_Android()
        {
            InitializeComponent();
            BindingContext = Resolver.Resolve<MainViewModel>();
        }
    }
}