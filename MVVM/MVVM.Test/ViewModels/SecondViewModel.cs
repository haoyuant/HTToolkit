using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HTToolkit.MVVM;
using Xamarin.Forms;

namespace MVVM.Test.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        public SecondViewModel()
        {
        }

        public ICommand GoToThirdViewCmd => new Command(() =>
        {
            NavigationService.NavigateToAsync<ThirdViewModel>("FROM FIRSTVIEW");
        });

        public ICommand GoBackCmd => new Command(() =>
        {
            NavigationService.NavigateBackAsync("FROM SECONDVIEW");
        });

        public override Task OnActivated(object parameter)
        {
            Debug.WriteLine("Rceived parameter {0}", parameter);
            return Task.CompletedTask;
        }
    }
}
