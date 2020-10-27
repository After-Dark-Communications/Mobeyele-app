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
    public partial class CallKeyPage : ContentPage
    {
        public CallKeyPage()
        {
            InitializeComponent();

            this.BindingContext = GetAccessableDoors();
        }
        //either per timeframe, when the user logs in or when the app is re-opened

        public async void LoadAccessableDoors()
        {
            //create new container for every door.
        }

        private void AccessableDoorsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        private List<string> GetAccessableDoors()
        {
            List<string> temp = new List<string>();
            temp.Add("Door1");
            temp.Add("Door2");
            temp.Add("Door3");
            temp.Add("Door4");
            temp.Add("Door5");
            temp.Add("Door6");
            temp.Add("Door7");
            return temp;
        }
    }
}