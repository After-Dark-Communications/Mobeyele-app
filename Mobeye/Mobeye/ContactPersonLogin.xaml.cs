using Mobeye.Logic;
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
                User user = new User();
                //if (user.LogInWithAccessCode(EnteredCode.Text) != null)
                //{
                    if (EnteredCode.Text == CallKeyCode())//TODO: replace this with proper check for permission
                    {
                        GoToCallKeyPage();
                    }
                    else if (EnteredCode.Text == ContactPersonCode())//TODO: replace this with proper check for permission
                    {
                        DisplayAlert("Contact Person", "You are now logged in as a contact person. You are now able to receive messages from any devices assigned to you via this app.", "OK");
                    }
                //}
                else
                {
                    DisplayAlert("Wrong code", "We could not verify the code \""+EnteredCode.Text+"\" to be correct", "Ok");
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