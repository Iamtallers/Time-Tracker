using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Time_Tracker.PageModels.Base;
using Xamarin.Forms;

namespace Time_Tracker.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public Task GoBackAsync()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

        public async Task NavigateToAsync<TPageModelBase>(object navigationData = null, bool setRoot = false)
        {
            var page = PageModelLocator.CreatePageFor(typeof(TPageModelBase));

            if (setRoot)
            {
                App.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                if (App.Current.MainPage is NavigationPage navPage)
                {
                    await navPage.PushAsync(page);
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(page);
                }
            }

            if (page.BindingContext is PageModelBase pmBase)
            {
                await pmBase.InitializeAsync(navigationData);
            }
        }
    }
}
