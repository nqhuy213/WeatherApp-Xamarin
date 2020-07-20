using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForecastApp
{
    public class Resolver
    {
        private static IContainer container;
        public static void Initialize(IContainer container)
        {
            Resolver.container = container;
        }
        public static T Resolve<T>()
        {
            return Resolver.container.Resolve<T>();
        }
    }
}
