using Mobeye.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private readonly UserModel usermodel;
        public Page1(UserModel usermodel)
        {
            InitializeComponent();
            this.usermodel = (usermodel == null) ? new UserModel() : usermodel;
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://mymobeye.eu/", BrowserLaunchMode.External);
        }

        private void ShowCallKeyPage_Clicked(object sender, EventArgs e)
        {
            this.GoToCallKeyPage(usermodel);
        }
        async void GoToCallKeyPage(UserModel model)
        {
            await Navigation.PushAsync(new CallKeyPage(model));
        }
    }
}