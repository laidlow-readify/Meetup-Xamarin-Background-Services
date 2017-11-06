using System;
using Serilog;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App.Job;
using Xamarin.Forms;

namespace XamarinBGServ.Droid
{
    [Activity(Label = "XamarinBGServ.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int JobId = 1;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.AndroidLog()
                .Enrich.WithProperty(Serilog.Core.Constants.SourceContextPropertyName, "XamarinBGServLog")
                .CreateLogger();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            JobScheduler jobScheduler = (JobScheduler) GetSystemService(JobSchedulerService);
            var jobBuilder = new JobInfo.Builder(JobId, new ComponentName(this, Java.Lang.Class.FromType(typeof(ExampleJobService))));
            jobBuilder.SetPeriodic(TimeSpan.FromMinutes(3).Milliseconds);
            jobBuilder.SetOverrideDeadline(TimeSpan.FromMinutes(1).Milliseconds);
            jobBuilder.SetMinimumLatency(TimeSpan.FromMinutes(1).Milliseconds);
            jobBuilder.SetTriggerContentMaxDelay(TimeSpan.FromMinutes(1).Milliseconds);

            var job = jobBuilder.Build();
            var result = jobScheduler.Schedule(job);
            MessagingCenter.Send<XamarinBGServ.App, string>(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                                            Messages.ConsoleMessage, result == JobScheduler.ResultSuccess ? "Job scheduled" : "Job scheduling failed");

        }
    }
}
