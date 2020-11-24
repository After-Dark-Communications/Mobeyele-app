using Mobeye.Dependency;
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

        internal void EnteredCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EnteredCode.Text.Length >= EnteredCode.MaxLength)
            {
                authLoad.IsRunning = true;
                User user = new User();
                UserModel res = user.LogInWithAccessCode(EnteredCode.Text);
                if (res != null)
                {
                    authLoad.IsRunning = false;
                    switch (res.PermissionLevel)
                    {
                        case 2:
                            DisplayAlert("Contact Person", "You are now logged in as a contact person. You are now able to receive messages from any devices assigned to you via this app.", "OK");
                            //TODO: add actual functionality.
                            break;
                        case 3:
                            GoToCallKeyPage();
                            break;
                        default:
                            DisplayAlert("Unauthorized", "You are not authorized to access any of these functions with the provided code. If you believe this to be wrong, please contact your code provider and try again.", "OK");
                            return;
                    }
                }
                else
                {
                    authLoad.IsRunning = false;
                    DisplayAlert("Wrong code", "We could not verify the code \""+EnteredCode.Text+"\" to be correct", "Ok");
                    EnteredCode.Text = "";
                }
            }
        }
    }
}