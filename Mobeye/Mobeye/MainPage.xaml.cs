using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
            tryInternet();
            base.OnAppearing();
        }

        async void OnPortalLoginClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PortalLogin());
        }

        async void OnContactPersonLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPersonLogin());
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            webload.IsRunning = true;
            using (HttpResponseMessage response = await APIHelper.API.GetAsync("users/?SmsKey=72940"))
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
        async void Retry_Connection(object sender, EventArgs e)
        {
            tryInternet();
        }

        private async void tryInternet()
        {
            webload.IsRunning = true;
            NetworkAccess netStatus = Connectivity.NetworkAccess;
            if (netStatus == NetworkAccess.Internet)
            {
#if DEBUG
                using (HttpResponseMessage response = await APIHelper.API.GetAsync("https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/posts"))
#else
                using (HttpResponseMessage response = await APIHelper.API.GetAsync("https://www.google.nl/"))//TODO: make test call to mobeye api
#endif
                {
                    if (response.IsSuccessStatusCode)
                    {
                        await Navigation.PushAsync(new ContactPersonLogin());
                    }
                    //return error message
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
