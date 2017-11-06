using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinBGServ.Models;
using System.Threading.Tasks;
using System.Threading;

namespace XamarinBGServ.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App()); 
            return base.FinishedLaunching(app, options);
        }

        public async override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            var item = new LoggerItem()
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Content = "PerformFetch called"
            };
            await App.Database.SaveItemAsync(item);

            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}
