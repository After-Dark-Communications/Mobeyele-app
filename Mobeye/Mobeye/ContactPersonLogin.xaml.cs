using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPersonLogin : ContentPage
    {
        public ContactPersonLogin()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        async void GoToCallKeyPage()
        {
            await Navigation.PushAsync(new CallKeyPage());
        }

        private void EnteredCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EnteredCode.Text.Length >= EnteredCode.MaxLength)
            {
                if (EnteredCode.Text == CallKeyCode())
                {
                    GoToCallKeyPage();
                }
                else if(EnteredCode.Text == ContactPersonCode())
                {
                    DisplayAlert("Contact Person", "You are now logged in as a contact person, you are now able to receive messages from any devices assigned to you via this app.", "OK");
                }
                else
                {
                    DisplayAlert("Wrong code", "We could not verify this code to be correct", "Ok");
                    EnteredCode.Text = "";
                }
            }
        }

        private string ContactPersonCode()
        {
            return "67890";
        }

        private string CallKeyCode()
        {
            return "12345";
        }
    }
}