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

        async void Button_Clicked(object sender, EventArgs e)
        {
            //gonna give that a shot
            webload.IsRunning = true;
            using (HttpResponseMessage response = await APIHelper.API.GetAsync("profile/?emailaddress=mobeye@test.nl"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    await DisplayAlert("success",contents,"OK");
                }
                else
                {
                    await DisplayAlert("failed", response.ReasonPhrase, "OK");
                }
            }
            webload.IsRunning = false;
        }
    }
}
