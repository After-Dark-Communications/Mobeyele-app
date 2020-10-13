using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortalLogin : ContentPage
    {
        private string username, password;

        public PortalLogin()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        async void OpenURL(object sender, EventArgs e) => await openTestSite();

        async void LoginWithPortalAccount(object sender, EventArgs e)
        {
            setUsernameAndPassword(Username.Text, Password.Text);
            //pass through to Logic to check if matching username and password
            if (quicktest(username, password))
            {
               await openTestSite();
            }
            else
            {
                await DisplayAlert("Couldn't Log In.", "The username/password were incorrect", "OK");
            }
        }

        private void setUsernameAndPassword(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        private bool quicktest(string username, string password)
        {
            if (username == "john.doe@mail.com" && password == "123456")
            {
                return true;
            }
            return false;
        }

        private Task openTestSite()
        {
           return Browser.OpenAsync("https://www.technetgroup.nl", BrowserLaunchMode.External);
        }
    }

}