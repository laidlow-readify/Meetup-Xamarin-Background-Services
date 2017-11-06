using System;
namespace XamarinBGServ
{
    public interface IPeriodicTask
    {
        void SchedulePeriodicWork();
        void CancelScheduledWork();
    }
}
