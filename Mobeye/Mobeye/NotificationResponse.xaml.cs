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
    public partial class NotificationResponse : ContentPage
    {
        public NotificationResponse()
        {
            InitializeComponent();
            AlertMessage.Text = "PLACEHOLDER TEXT";
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public NotificationResponse(string message)
        {
            InitializeComponent();
            AlertMessage.Text = message;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        async void ContinueButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPersonLogin());
        }
    }
}