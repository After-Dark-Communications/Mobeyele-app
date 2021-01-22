using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateKeyPage : ContentPage
    {
        public PrivateKeyPage()
        {
            InitializeComponent();
        }

        public PrivateKeyPage(string privateKey)
        {
            InitializeComponent();
            PrivateKey.Text = privateKey;
        }

        private void RevealCodeButton_Clicked(object sender, EventArgs e)
        {
            RevealCodeButton.IsVisible = false;
            PrivateKey.IsVisible = true;
        }

        private async void ContinueButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactPersonLogin());
        }
    }
}