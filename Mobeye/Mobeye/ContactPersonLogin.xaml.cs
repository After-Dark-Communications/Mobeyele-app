using Mobeye.Dependency;
using Mobeye.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPersonLogin : ContentPage
    {
        TypeAssistant assistant;
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        public ContactPersonLogin()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }


        internal void EnteredCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token) // if no keystroke occurs, carry on after 1s
           .ContinueWith(
               delegate { AttemptLogin(); }, // Pass the changed text to the PerformSearch function
               CancellationToken.None,
               TaskContinuationOptions.OnlyOnRanToCompletion,
               TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void EnteredCode_Completed(object sender, EventArgs e)
        {
            authLoad.IsRunning = true;
            if (EnteredCode.Text.Length == 4)
            {
                User user = new User(Device.RuntimePlatform);
                UserModel res = user.LogInWithPrivateKey(EnteredCode.Text); ;
                BringUserToAuthorizedSection(EnteredCode.Text, user, res);
            }
            else if (EnteredCode.Text.Length >= 5)
            {
                AttemptLogin();
            }
            else
            {
                DisplayAlert("Too short", "The code you entered is too short, please verify that this is the correct code and try again.", "Ok");
            }
        }

        private void AttemptLogin()
        {
            //perhaps do 4 or 5 length check now
            authLoad.IsRunning = true;
            if (EnteredCode.Text.Length >= EnteredCode.MaxLength)
            {
                User user = new User(Device.RuntimePlatform);
                UserModel res = null;
                res = user.LogInWithPrivateKey(EnteredCode.Text);

                if (res != null)
                {
                    BringUserToAuthorizedSection(EnteredCode.Text, user, res);
                }
                else
                {
                    if (successfullRegister(user))
                    {
                        UserModel _user = user.createMinimalUM(EnteredCode.Text, user.Register(EnteredCode.Text));
                        BringUserToAuthorizedSection(_user.PrivateKey, user);
                        //UserModel _user = new UserModel(EnteredCode.Text,user.Register(EnteredCode.Text),"","","",2);
                    }
                    else
                    {
                        DisplayAlert("Wrong code", "We could not verify the code \"" + EnteredCode.Text + "\" to be correct", "Ok");
                        EnteredCode.Text = "";
                    }
                }

            }
            authLoad.IsRunning = false;
        }

        private Task openTestSite()
        {
            return Browser.OpenAsync("https://www.technetgroup.nl", BrowserLaunchMode.External);
        }

        private bool successfullRegister(User user)
        {
            string res = user.Register(EnteredCode.Text);
            if (!string.IsNullOrWhiteSpace(res))
            {
                return true;
            }
            return false;
        }
        private void BringUserToAuthorizedSection(string code, User user, UserModel res = null)
        {
            if (res == null)
            {
                res = user.LogInWithPrivateKey(code);
            }
            if (res != null)
            {
                authLoad.IsRunning = false;
                switch (res.PermissionLevel)
                {
                    case 1:
                        openTestSite();
                        break;
                    case 2:
                        DisplayAlert("Contact Person", "You are now logged in as a contact person. You are now able to receive messages from any devices assigned to you via this app.", "OK");
                        //TODO: add actual functionality.
                        break;
                    case 3:
                        GoToCallKeyPage();
                        break;
                    default:
                        DisplayAlert("Unauthorized", "You are not authorized to access any of these functions with the provided code. If you believe this to be wrong, please contact your code provider and try again.", "OK");
                        EnteredCode.Text = "";
                        return;
                }
            }
            else
            {
                DisplayAlert("Couldn't Authorize", "We were unable to authorize you.", "OK");
                EnteredCode.Text = "";
            }
            authLoad.IsRunning = false;
        }
        async void GoToCallKeyPage()
        {
            await Navigation.PushAsync(new CallKeyPage());
        }

    }
}