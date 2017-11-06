using System;
using Android.App.Job;
using Android.Content;
using Xamarin.Forms;
using XamarinBGServ.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PeriodicTask))]
namespace XamarinBGServ.Droid
{
    public class PeriodicTask : IPeriodicTask
    {
        private const int JobId = 99;

        public void CancelScheduledWork()
        {
            JobScheduler jobScheduler = (JobScheduler)Xamarin.Forms.Forms.Context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Cancel(JobId);
            MessagingCenter.Send<XamarinBGServ.App, string>(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                                            Messages.ConsoleMessage, "Job cancelled");
        }

        public void SchedulePeriodicWork()
        {
            JobScheduler jobScheduler = (JobScheduler)Xamarin.Forms.Forms.Context.GetSystemService(Context.JobSchedulerService);
            var jobBuilder = new JobInfo.Builder(JobId, new ComponentName(Xamarin.Forms.Forms.Context, Java.Lang.Class.FromType(typeof(ExampleJobService))));
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
