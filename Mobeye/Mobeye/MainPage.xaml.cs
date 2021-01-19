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
                using (HttpResponseMessage response = ApiHelper.Api.GetAsync("https://mymobeye.eu/".Result))//TODO: make test call to mobeye api
#endif
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                       
                    }
                }
                webload.IsRunning = false;
                DisplayAlert("No Connection", "Unable to connect to the api, please check your internet connection and try again. if this issue persists, please contact the mobeye company", "OK");
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
