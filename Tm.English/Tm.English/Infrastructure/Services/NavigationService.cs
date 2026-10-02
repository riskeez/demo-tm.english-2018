using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Mobile.Core;
using Tm.Mobile.Core.Abstract;
using Xamarin.Forms;

namespace Tm.English.Services
{
    public class NavigationService : INavigationService
    {
        Dictionary<string, Type> Pages { get; set; }

        Page RootPage => Application.Current.MainPage;

        public NavigationService()
        {
            Pages = new Dictionary<string, Type>();
        }

        public string CurrentPageKey
        {
            get
            {
                var currentModalPage = RootPage.Navigation.ModalStack.LastOrDefault()?.GetType()?.Name;
                if (!string.IsNullOrEmpty(currentModalPage))
                    return currentModalPage;

                return RootPage.Navigation.NavigationStack.LastOrDefault()?.GetType()?.Name;
            }
        }

        public void Configure(string pageName, Type pageType)
        {
            if (Pages.ContainsKey(pageName))
            {
                Pages[pageName] = pageType;
            }
            else
            {
                Pages.Add(pageName, pageType);
            }
        }

        public async Task GoBack()
        {
            if (RootPage.Navigation.ModalStack.Count > 0)
            {
                await RootPage.Navigation.PopModalAsync(false);
            }
            else
            {
                await RootPage.Navigation.PopAsync(true);
            }
        }

        public async Task NavigateTo<TViewModel>(string pageKey, bool forceNavigate = false)
            where TViewModel : ViewModelBase
        {
            await Navigate(pageKey, ViewModelLocator.GetInstance<TViewModel>(), false, false);
        }

        public async Task NavigateTo(string pageKey, ViewModelBase viewModel, bool forceNavigate = false)
        {
            await Navigate(pageKey, viewModel, false, false);
        }

        private async Task Navigate(string pageKey, ViewModelBase viewModel, bool forceNavigate, bool animation)
        {
            if (!forceNavigate && !CanNavigate(pageKey))
                return;

            Page page = CreatePage(pageKey, null);

            page.BindingContext = viewModel;

            await GoToPageAsync(page, animation);
        }

        private async Task GoToPageAsync(Page page, bool animation)
        {
            switch (page)
            {
                case IModalPage modal:
                    await RootPage.Navigation.PushModalAsync(page, animation);
                    break;
                default:
                    await RootPage.Navigation.PushAsync(page, animation);
                    break;
            }
        }

        private Page CreatePage(string pageKey, object parameter)
        {
            Page page = null;
            try
            {
                object[] pars = parameter != null ? new object[] { parameter } : new object[] { };
                page = (Page)Activator.CreateInstance(Pages[pageKey], pars);

                return page;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return page;
        }

        private bool CanNavigate(string pageKey)
        {
            if (CurrentPageKey == pageKey)
                return false;

            return true;
        }
    }
}
