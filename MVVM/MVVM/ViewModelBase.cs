using System;
using System.Threading.Tasks;
using Autofac;

namespace HTToolkit.MVVM
{
    public class ViewModelBase : Bindable
    {
        bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public INavigationService NavigationService { get; set; }

        public ViewModelBase()
        {
            NavigationService = new HierarchyNavigationService();
        }

        public virtual Task OnActivated(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
