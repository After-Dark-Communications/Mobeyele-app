using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public void RefreshDoors(object sender, EventArgs e)
        {
            List<string> Doors = GetAccessableDoors();
            DoorContainer.Children.Clear();
            for (int i = 0; i < Doors.Count; i++)
            {
                DoorContainer.Children.Add(CreateNewDoorItem(Doors[i]));
            }
        }

        private async Task MockOpenDoor(string Doorname)
        {

            //OpeningLabel.IsVisible = true;
            //TODO: add text "Opening door 'X'..." with increasing elipses
            //OpeningLabel.Text = $"Opening \"{Doorname}\"";
            //await Task.Delay(sleepTime);
            //OpeningLabel.Text += ".";
            //await Task.Delay(sleepTime);
            //OpeningLabel.Text += ".";
            //await Task.Delay(sleepTime);
            //OpeningLabel.Text += ".";
            //await Task.Delay(sleepTime);
            //OpeningLabel.Text = "";
            try
            {
                //ShowProgresBar("Loading...");
                OpeningLabel.Text = $"Opening \"{Doorname}\"";

                for (int i = 0; i < 4; i++)
                {
                    await Task.Run(() =>
                    {
                        return Task.Delay(1000);
                    });
                    OpeningLabel.Text += ".";
                }
                OpeningLabel.Text = $"Opened Door \"{Doorname}\"";

            }
            catch (Exception ex)
            {

            }
            //OpeningLabel.IsVisible = false;
        }

        private Frame CreateNewDoorItem(string name)
        {
            Frame frame = new Frame()
            {
                Padding = new Thickness(20, 20, 20, 10),
                BackgroundColor = Color.FromHex("#E5F5F5F5"),
                CornerRadius = 5,
            };

            Grid grid = new Grid();
            Label nameLabel = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                TextColor = Color.FromHex("#080808"),
                Text = "Name:",
            };
            Label doorLabel = new Label
            {
                Margin = new Thickness(60, 0, 0, 0),
                FontSize = 18,
                TextColor = Color.FromHex("#000000"),
                Text = name,
            };
            Button button = new Button
            {
                Margin = new Thickness(0, 30, 0, 0),
                Text = "Open",

            };
            //TODO: add button clicked event to mockopendoor
            //button.Clicked += Button_Clicked;
            grid.Children.Add(nameLabel);
            grid.Children.Add(doorLabel);
            grid.Children.Add(button);
            frame.Content = grid;
            return frame;
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await MockOpenDoor("Door1");
        }
    }//different part
}