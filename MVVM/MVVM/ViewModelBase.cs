using System;
using System.Threading.Tasks;
using Autofac;

namespace HTToolkit.MVVM
{
    public class ViewModelBase
    {
        public INavigationService NavigationService { get; set; }

        public ViewModelBase()
        {
            NavigationService = ServiceLocator.Resolve<INavigationService>();
        }

        public virtual Task OnActivated(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
