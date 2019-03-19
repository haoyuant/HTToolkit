using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HTToolkit.MVVM
{
    public class HierarchyNavigationService : INavigationService
    {
        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateBackAsync(object parameter = null)
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                await InternalNavigateBack(parameter, navPage);
            }
            else if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.Detail is NavigationPage detailNavPage)
                {
                    await InternalNavigateBack(parameter, detailNavPage);
                }
            }
        }

        static async Task InternalNavigateBack(object parameter, NavigationPage navPage)
        {
            var stack = navPage.Navigation.NavigationStack;
            if (stack.Count >= 2)
            {
                await navPage.PopAsync();
                var currentPage = stack[stack.Count - 1];
                await (currentPage.BindingContext as ViewModelBase).OnActivated(parameter);
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                InternalRemoveLastFromBackStack(navPage);
            }
            else if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.Detail is NavigationPage detailNavPage)
                {
                    InternalRemoveLastFromBackStack(detailNavPage);
                }
            }
            return Task.FromResult(true);
        }

        static void InternalRemoveLastFromBackStack(NavigationPage navPage)
        {
            if (navPage.Navigation.NavigationStack.Count >= 2)
            {
                navPage.Navigation.RemovePage(
                    navPage.Navigation.NavigationStack[navPage.Navigation.NavigationStack.Count - 2]);
            }
        }

        public Task ClearBackStackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                InternalClearBackStack(navPage);
            }
            else if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.Detail is NavigationPage detailNavPage)
                {
                    InternalClearBackStack(detailNavPage);
                }
            }

            return Task.FromResult(true);
        }

        static void InternalClearBackStack(NavigationPage navPage)
        {
            for (int i = 0; i < navPage.Navigation.NavigationStack.Count - 1; i++)
            {
                var page = navPage.Navigation.NavigationStack[i];
                navPage.Navigation.RemovePage(page);
            }
        }

        async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                if (masterDetailPage.Detail is NavigationPage navPage)
                {
                    await navPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(page);
                }
            }
            await (page.BindingContext as ViewModelBase).OnActivated(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public Task RedirectTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            Page page = CreatePage(typeof(TViewModel));
            Application.Current.MainPage = page;
            ((ViewModelBase)page.BindingContext)?.OnActivated(parameter);

            return Task.CompletedTask;
        }
    }
}
