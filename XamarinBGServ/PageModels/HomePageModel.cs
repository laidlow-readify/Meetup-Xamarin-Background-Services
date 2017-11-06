using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;
using XamarinBGServ.Models;
using Serilog;

namespace XamarinBGServ.PageModels
{
    public class HomePageModel : FreshBasePageModel
    {
        public string Title { get; set; }
        public ObservableCollection<LoggerItem> LoggerListItems { get; set; }
        public Command RefreshConsoleCommand { get; set; }
        public Command StartStopServiceCommand { get; set; }
        public string ButtonText { get; set; }
        public bool ServiceStarted { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Title = "Xamarin Background Services";
            ButtonText = "Start background service";
            RefreshConsoleCommand = new Command(RefreshConsole);
            StartStopServiceCommand = new Command(StartStopService);
            LoggerListItems = new ObservableCollection<LoggerItem>();
            SubscribeToMessages();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await UpdateConsoleWindow();
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<XamarinBGServ.App, string>(this, Messages.ConsoleMessage, (sender, message) =>
            {
                AddLineToConsole(message);
            });
        }

        private void StartStopService()
        {
            ServiceStarted = !ServiceStarted;
            ButtonText = ServiceStarted ? "Stop background service" : "Start background service";
            RaisePropertyChanged(nameof(ButtonText));

            var periodicTask = DependencyService.Get<IPeriodicTask>();
            if (ServiceStarted)
            {
                periodicTask.SchedulePeriodicWork();
            }
            else
            {
                periodicTask.CancelScheduledWork();
            }
        }

        private void RefreshConsole()
        {
            MessagingCenter.Send(Xamarin.Forms.Application.Current as XamarinBGServ.App,
                                 Messages.RefreshConsole);
        }

        private async Task UpdateConsoleWindow()
        {
            var items = await App.Database.GetItemsAsync();
            items.Reverse();
            LoggerListItems.Clear();
            RaisePropertyChanged(nameof(LoggerListItems));
            foreach (LoggerItem item in items)
            {
                LoggerListItems.Add(item);
            }
            RaisePropertyChanged(nameof(LoggerListItems));
        }

        private void AddLineToConsole(string message)
        {
            var item = new LoggerItem()
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Content = message
            };
            LoggerListItems.Insert(0, item);
            Log.Information(item.Content);
            App.Database.SaveItemAsync(item);
        }
    }
}
