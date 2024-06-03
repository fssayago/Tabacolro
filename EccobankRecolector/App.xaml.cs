using EccobankRecolector.Vista;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SharedTransitions;
namespace EccobankRecolector
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new SharedTransitionNavigationPage(new Login());
            //MainPage = new SharedTransitionNavigationPage(new Menuprincipal());
            Application.Current.MainPage = new NavigationPage(new Login());
            //MainPage = new SharedTransitionNavigationPage(new RegCompras());
            //Application.Current.MainPage = new NavigationPage(new RegCompras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
