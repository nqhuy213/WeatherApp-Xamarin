using Autofac;
using ForecastApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ForecastApp
{
    public class Bootstrapper
    {
        protected ContainerBuilder containerBuilder { get; private set; }

        public Bootstrapper()
        {
            Init();
            FinishInitialization();
        }
        protected virtual void Init()
        {
            containerBuilder = new ContainerBuilder();
            var currentAssembly = Assembly.GetExecutingAssembly();
            foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Page)) || e.IsSubclassOf(typeof(BaseViewModel))))
            {
                containerBuilder.RegisterType(type.AsType());
            }
            
        }
        public void FinishInitialization()
        {
            var container = containerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
