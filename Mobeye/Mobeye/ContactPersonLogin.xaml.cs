﻿using Mobeye.Dependency;
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

        async void GoToHomePage(UserModel model)
        {
            await Navigation.PushAsync(new Page1(model));
        }

        async void GoToPrivateKeyPage(string privateKey)
        {
            await Navigation.PushAsync(new PrivateKeyPage(privateKey));
        }

        internal void EnteredCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token) // if no keystroke occurs, carry on after 500ms
           .ContinueWith(
               delegate
               {                   
                   if (EnteredCode.Text.Length >= 4) { validateButton.IsVisible = true; }
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
            string privateKey = user.Register(EnteredCode.Text);
            UserModel res = user.LogInWithPrivateKey(privateKey);
            if (res != null)
            {
                BringUserToAuthorizedSection(EnteredCode.Text, user, res);
            }
            else
            {
                authLoad.IsRunning = false;
                if (successfullRegister(user))
                {
                    UserModel _user = user.createMinimalUm(EnteredCode.Text, user.Register(EnteredCode.Text));
                    GoToPrivateKeyPage(_user.PrivateKey);
                }
                else
                {
                    DisplayAlert("Wrong code", "We could not verify the code \"" + EnteredCode.Text + "\" to be correct", "Ok");
                    EnteredCode.Text = "";
                }
            }
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
                switch (res.UserRole)
                {
                    case "Account":
                        GoToHomePage(res);
                        break;
                    case "Standard":
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