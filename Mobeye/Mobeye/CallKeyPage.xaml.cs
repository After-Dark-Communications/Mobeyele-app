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
       //do we have that in the questions doc?
        public async void LoadAccessableDoors()
        {
            //create new container for every door.
            
        }

        public async void RefreshDoors(object sender, EventArgs e)
        {
            List<string> Doors = GetAccessableDoors();
            DoorContainer.Children.Clear();
            for (int i = 0; i < Doors.Count; i++)
            {
                DoorContainer.Children.Add(CreateNewDoorGrid(Doors[i]));
            }
        }

        private void AccessableDoorsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //do we know the location of a call key?
        }

        private Grid CreateNewDoorGrid(string name)
        {
            Grid temp = new Grid
            {
                BackgroundColor = Color.FromHex("#7FFFFFFF"),
            Padding = 10,
            };
            Label nameLabel = new Label
            { 
               FontAttributes = FontAttributes.Bold,
               FontSize = 18,
               Text = "Name:",
            };
            Label doorLabel = new Label
            {
                Margin = new Thickness(60, 0, 0, 0),
                FontSize = 18,
                Text = name,
            };
            Button button = new Button
            {
                Margin = new Thickness(0, 30, 0, 0),
                Text = "Open",
            };
            temp.Children.Add(nameLabel);
            temp.Children.Add(doorLabel);
            temp.Children.Add(button);
            return temp;
        }

        private List<string> GetAccessableDoors()
        {
            List<string> temp = new List<string>();
            for (int i = 1; i < 14; i++)
            {
                temp.Add("Door" + i);
            }
            return temp;
        }
    }//different part
}