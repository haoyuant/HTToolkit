using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HTToolkit.MVVM;
using Xamarin.Forms;

namespace MVVM.Test.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        public FirstViewModel()
        {
        }

        public ICommand GoToSecondViewCmd => new Command(() =>
        {
            NavigationService.NavigateToAsync<SecondViewModel>("FROM FIRSTVIEW");
        });

        public ICommand GoBackCmd => new Command(() =>
        {
            NavigationService.NavigateBackAsync("FROM FIRSTVIEW");
        });

        public override Task OnActivated(object parameter)
        {
            Debug.WriteLine("Rceived parameter {0}", parameter);
            return Task.CompletedTask;
        }
    }
}
