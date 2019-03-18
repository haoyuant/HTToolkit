using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HTToolkit.MVVM
{
    public class HierarchyNavigationService : INavigationService
    {
        public Task NavigateToAsync<TViewModel>(object parameter = null)
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task NavigateBackAsync(object parameter = null)
        {
            if (Application.Current.MainPage is NavigationPage mainPage)
            {
                var stack = mainPage.Navigation.NavigationStack;
                if (stack.Count >= 2)
                {
                    await mainPage.PopAsync();
                    var currentPage = stack[stack.Count - 1];
                    await (currentPage.BindingContext as ViewModelBase).OnActivated(parameter);
                }
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            if (Application.Current.MainPage is NavigationPage mainPage)
            {
                if (mainPage.Navigation.NavigationStack.Count >= 2)
                {
                    mainPage.Navigation.RemovePage(
                        mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
                }
            }

            return Task.FromResult(true);
        }

        public Task ClearBackStackAsync()
        {
            if (Application.Current.MainPage is NavigationPage mainPage)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
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
    }
}
