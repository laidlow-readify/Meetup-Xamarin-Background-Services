using System;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using XamarinBGServ.Models;
using Serilog;

namespace XamarinBGServ.Droid
{
    [Service(Enabled = true, Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
    public class ExampleJobService : JobService
    {
        public override bool OnStartJob(JobParameters @params)
        {
            try
            {
                Task.Run(async () => {
                    try
                    {
                        var item = new LoggerItem()
                        {
                            Time = DateTime.Now.ToString("HH:mm:ss"),
                            Content = "JobService OnStartJob called"
                        };
                        Log.Information(item.Content);
                        await App.Database.SaveItemAsync(item);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            finally
            {
                JobFinished(@params, needsReschedule: false);
            }

            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }
    }
}
