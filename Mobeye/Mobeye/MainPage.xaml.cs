using System;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using System.Net.Http;
using Mobeye.API;
using Xamarin.Essentials;

namespace Mobeye
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        protected override void OnAppearing()
        {
            //TODO: check if user has internet connection. If so, check if user can connect with mobeye or whomever is the api provider
            TryInternet();
            base.OnAppearing();
        }

        private async void OnPortalLoginClick(EventArgs e)
        {
            await Navigation.PushAsync(new PortalLogin());
        }

        private async void OnContactPersonLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPersonLogin());
        }

        private async void Button_Clicked(EventArgs e)
        {
            webload.IsRunning = true;
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync("users/?SmsKey=72940"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    await DisplayAlert("success", contents, "OK");
                }
                else
                {
                    await DisplayAlert("failed", response.ReasonPhrase, "OK");
                }
            }
            webload.IsRunning = false;
        }

        private void Retry_Connection(object sender, EventArgs e)
        {
            TryInternet();
        }

        private async void TryInternet()
        {
            webload.IsRunning = true;
            NetworkAccess netStatus = Connectivity.NetworkAccess;
            if (netStatus == NetworkAccess.Internet)
            {
                using (HttpResponseMessage response = await ApiHelper.Api.GetAsync("https://www.google.nl/"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new ContactPersonLogin());
                    }
                }
                webload.IsRunning = false;
            }
            else
            {
                await DisplayAlert("No Internet", "Unable to connect to the internet, please check your connection and try again.", "OK");
                webload.IsRunning = false;
                retryButton.IsVisible = true;
            }
        }
    }
}
