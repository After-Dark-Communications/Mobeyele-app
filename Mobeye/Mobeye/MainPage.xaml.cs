using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace Mobeye
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        async void OnPortalLoginClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PortalLogin());
        }

        async void OnContactPersonLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPersonLogin());
        }
    }
}
