using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HTToolkit.MVVM
{
    public interface INavigationService
    {
        Task RedirectTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
        Task NavigateBackAsync(object parameter = null);
        Task RemoveLastFromBackStackAsync();
        Task ClearBackStackAsync();
    }
}
