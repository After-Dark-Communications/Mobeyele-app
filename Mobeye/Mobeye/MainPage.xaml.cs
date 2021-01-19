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
        TypeAssistant assistant;
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            assistant = new TypeAssistant();
        }

        protected override void OnAppearing()
        {
            //TODO: check if user has internet connection. If so, check if user can connect with mobeye or whomever is the api provider

            if (TryInternet())
            {
                Navigation.PushAsync(new ContactPersonLogin());
            }
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
        async void Retry_Connection(object sender, EventArgs e)
        {
            TryInternet();
        }

        private bool TryInternet()
        {
            webload.IsRunning = true;
            NetworkAccess netStatus = Connectivity.NetworkAccess;
            if (netStatus == NetworkAccess.Internet)
            {
#if DEBUG
                using (HttpResponseMessage response = ApiHelper.Api.GetAsync("https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/posts").Result)
#else
                using (HttpResponseMessage response = await ApiHelper.Api.GetAsync("https://www.google.nl/"))//TODO: make test call to mobeye api
#endif
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                       
                    }
                
                    //return error message
                }
                webload.IsRunning = false;
                return false;
            }
            else
            {
                DisplayAlert("No Internet", "Unable to connect to the internet, please check your connection and try again.", "OK");
                webload.IsRunning = false;
                retryButton.IsVisible = true;
                return false;
            }
        }
    }
}
