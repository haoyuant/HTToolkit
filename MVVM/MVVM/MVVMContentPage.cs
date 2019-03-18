using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Autofac;

namespace HTToolkit.MVVM
{
    public class MVVMContentPage : ContentPage
    {
        protected ViewModelBase viewModelBase;

        public MVVMContentPage()
        {
            AutoWireViewModel();
        }

        protected void AutoWireViewModel()
        {
            var viewType = GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = Activator.CreateInstance(viewModelType);
            BindingContext = viewModel;
        }

    }
}

