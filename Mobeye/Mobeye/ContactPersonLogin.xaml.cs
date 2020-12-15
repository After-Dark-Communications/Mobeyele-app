using Mobeye.Dependency;
using Mobeye.Logic;
using System;
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
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        public ContactPersonLogin()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        async void GoToCallKeyPage(UserModel model)
        {
            await Navigation.PushAsync(new CallKeyPage(model));
        }

        internal void EnteredCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token) // if no keystroke occurs, carry on after 500ms
           .ContinueWith(
               delegate
               {
                   if (EnteredCode.Text.Length >= 5) { AttemptLogin(); } //perform regular login
                   else if (EnteredCode.Text.Length >= 4) { validateButton.IsVisible = true; }//if 4 digits have been entered, show login button
                   else { validateButton.IsVisible = false; }//hide login button
               },
               CancellationToken.None,
               TaskContinuationOptions.OnlyOnRanToCompletion,
               TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void validateButton_Clicked(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void AttemptLogin()
        {
            authLoad.IsRunning = true;
            User user = new User(Device.RuntimePlatform);
            UserModel res = user.LogInWithPrivateKey(EnteredCode.Text);
            if (res != null)
            {
                BringUserToAuthorizedSection(EnteredCode.Text, user, res);
            }
            else
            {
                authLoad.IsRunning = false;
                if (successfullRegister(user))
                {
                    UserModel _user = user.createMinimalUM(EnteredCode.Text, user.Register(EnteredCode.Text));
                    BringUserToAuthorizedSection(_user.PrivateKey, user);
                }
                else
                {
                    DisplayAlert("Wrong code", "We could not verify the code \"" + EnteredCode.Text + "\" to be correct", "Ok");
                    EnteredCode.Text = "";
                }
            }
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
                        GoToCallKeyPage(res);
                        break;
                    default:
                        DisplayAlert("Unauthorized", "You are not authorized to access any of these functions with the provided code. If you believe this to be wrong, please contact your code provider and try again.", "OK");
                        return;
                }
            }
            else
            {
                DisplayAlert("Login failed", "We were unable to log you in, please try again later or contact the one who provided the code", "OK");
            }
        }

    }
}