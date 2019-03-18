using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MVVM.Test.Views;
using HTToolkit.MVVM;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MVVM.Test
{
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.Register<INavigationService, HierarchyNavigationService>(true);
            ServiceLocator.Build();

            InitializeComponent();
            MainPage = new NavigationPage(new MainView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
