using System;
using System.Threading.Tasks;
using Time_Tracker.PageModels;
using Time_Tracker.PageModels.Base;
using Time_Tracker.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Time_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            
        }

        Task InitNavigation()
        {
            var navService = PageModelLocator.Resolve<INavigationService>();
            return navService.NavigateToAsync<LoginPageModel>();
        }

        protected override async void OnStart()
        {
            await InitNavigation();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
