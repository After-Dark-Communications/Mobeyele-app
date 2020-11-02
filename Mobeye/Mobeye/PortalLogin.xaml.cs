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
using Mobeye.Logic;
using Mobeye.API;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortalLogin : ContentPage
    {
        public string email { get; private set; }
        public string password { get; private set; }
        public bool RememberCredentials { get; private set; }

        public PortalLogin()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        async void OpenURL(object sender, EventArgs e) => await openTestSite();

        internal void LoginWithPortalAccount(object sender, EventArgs e)
        {
            loginLoad.IsRunning = true;
            setUsernameAndPassword(Username.Text, Password.Text);
            //pass through to Logic to check if matching username and password
            
            
            User user = new User();
                if (user.LogInWithCredentials(email, password) != null)//TODO: add remember me functionality
                {
                    openTestSite();
                    loginLoad.IsRunning = false;
                }
                else
                {
                    DisplayAlert("Couldn't Log In.", "The username/password were incorrect", "OK");
                    loginLoad.IsRunning = false;
                }
            
        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RememberCredentials = RememberMe.IsChecked;//add user credentials to persistence
        }

        private void setUsernameAndPassword(string username, string password)
        {
            this.email = username;
            this.password = password;
        }

        #region testing only!
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
        #endregion


    }

}