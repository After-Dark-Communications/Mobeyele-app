using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Mobeye.API;

namespace Mobeye
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            APIHelper.InitaliazeClient();
            MainPage = new NavigationPage(new MainPage());
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
