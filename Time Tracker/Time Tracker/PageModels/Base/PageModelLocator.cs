using System;
using System.Collections.Generic;
using System.Text;
using Time_Tracker.Models;
using Time_Tracker.Pages;
using Time_Tracker.Services;
using Time_Tracker.Services.Account;
using Time_Tracker.Services.Navigation;
using TinyIoC;
using Xamarin.Forms;

namespace Time_Tracker.PageModels.Base
{
    public class PageModelLocator
    {
        static TinyIoCContainer _container;
        static Dictionary<Type, Type> _viewLookup;

        static PageModelLocator()
        {
            _container = new TinyIoCContainer();
            _viewLookup = new Dictionary<Type, Type>();

            //Register pages and pagemodels
            Register<LoginPageModel, LoginPage>();
            Register<DashboardPageModel, DashboardPage>();
            Register<ProfilePageModel, ProfilePage>();
            Register<SettingsPageModel, SettingsPage>();
            Register<SummaryPageModel, SummaryPage>();
            Register<TimeClockPageModel, TimeClockPagexaml>();


            //Register services (Services are registered as SIngletons by defualt)
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IAccountService>(DependencyService.Get<IAccountService>());
            _container.Register(DependencyService.Get<IRepository<TestData>>());
        }

        public static T Resolve<T>() where T : class 
        {
            return _container.Resolve<T>();
        }

        public static Page CreatePageFor(Type pageModelType)
        {
            var pageType = _viewLookup[pageModelType];
            var page = (Page)Activator.CreateInstance(pageType);
            var pageModel = _container.Resolve(pageModelType);
            page.BindingContext = pageModel;
            return page;
        }

        static void Register<TPageModel, TPage>() where TPageModel : PageModelBase where TPage : Page
        {
            _viewLookup.Add(typeof(TPageModel), typeof(TPage));
            _container.Register<TPageModel>();
        }

    }
}
