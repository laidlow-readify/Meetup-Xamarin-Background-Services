using UIKit;
using Xamarin.Forms;
using XamarinBGServ.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(PeriodicTask))]
namespace XamarinBGServ.iOS
{
    public class PeriodicTask : IPeriodicTask
    {
        public void CancelScheduledWork()
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalNever);
            MessagingCenter.Send<XamarinBGServ.App, string>(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                     Messages.ConsoleMessage, $"Background fetch disabled");
        }

        public void SchedulePeriodicWork()
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);
            MessagingCenter.Send<XamarinBGServ.App, string>(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                                 Messages.ConsoleMessage, $"Background fetch enabled");
        }
    }
}
