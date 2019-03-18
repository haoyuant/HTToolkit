using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HTToolkit.MVVM;
using Xamarin.Forms;

namespace MVVM.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public ICommand GoToFirstViewCmd => new Command(() =>
        {
            NavigationService.NavigateToAsync<FirstViewModel>("FROM MAINVIEW");
        });

        public override Task OnActivated(object parameter)
        {
            Debug.WriteLine("Rceived parameter {0}", parameter);
            return Task.CompletedTask;
        }
    }
}
