using System.Collections.Generic;
using App3.Repositories;
using App3.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App3
{
    public partial class App : Application
    {
        public static IList<string> MemoItems { get; set; }
        public static IMemoItemRepository MemoItemRepository;

        public App()
        {
            InitializeComponent();
            MemoItems = new List<string>();
            MainPage = new NavigationPage(new HistoryPage());
        }

        public static IMemoItemRepository MemoManager
        {
            get
            {
                if (MemoItemRepository == null)
                {
                    MemoItemRepository = new MemoItemRepository(DependencyService.Get<IFileHelper>().GetLocalFilePath("MemoSQLite.db3"));
                }

                return MemoItemRepository;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
