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

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
            LoadApplication(new App()); 
            MessagingCenter.Send<XamarinBGServ.App, string>(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                                 Messages.ConsoleMessage, $"Background fetch enabled");
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
