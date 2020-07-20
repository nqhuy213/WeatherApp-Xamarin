using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace ForecastApp.iOS
{
    public class Bootstrapper : ForecastApp.Bootstrapper
    {
        public static void Execute()
        {
            var instance = new Bootstrapper();
        }
        protected override void Init()
        {
            base.Init();
        }
    }
}