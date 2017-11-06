using FreshMvvm;
using Serilog;
using Xamarin.Forms;
using XamarinBGServ.PageModels;

namespace XamarinBGServ
{
    public partial class App : Application
    {
        static BGServDatabase database;

        public App()
        {
            InitializeComponent();

            var homePage = FreshPageModelResolver.ResolvePageModel<HomePageModel>();

            MainPage = new FreshNavigationContainer(homePage);
        }

        public static BGServDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BGServDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}
