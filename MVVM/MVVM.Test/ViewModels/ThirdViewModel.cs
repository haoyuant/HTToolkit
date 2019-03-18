using System;
using System.Windows.Input;
using HTToolkit.MVVM;
using Xamarin.Forms;

namespace MVVM.Test.ViewModels
{
    public class ThirdViewModel : ViewModelBase
    {
        public ThirdViewModel()
        {
        }

        public ICommand RemoveLastCmd => new Command(() =>
        {
            NavigationService.RemoveLastFromBackStackAsync();
        });

        public ICommand GoBackCmd => new Command(() =>
        {
            NavigationService.NavigateBackAsync("FROM THIRDVIEW");
        });
    }
}
