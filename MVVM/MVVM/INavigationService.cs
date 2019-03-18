using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HTToolkit.MVVM
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(object parameter = null);
        Task NavigateBackAsync(object parameter = null);
        Task RemoveLastFromBackStackAsync();
        Task ClearBackStackAsync();
    }
}
