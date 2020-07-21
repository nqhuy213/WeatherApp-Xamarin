using ForecastApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService NavigationService { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public BaseViewModel()
        {
            NavigationService = Resolver.Resolve<INavigationService>();
        }
        protected void Set<T>(ref T field, T value, [CallerMemberName] string prop = null)
        {
            field = value;
            OnPropertyChanged(prop);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public virtual Task InitializeAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
