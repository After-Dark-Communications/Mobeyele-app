using Mobeye.Dependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobeye
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallKeyPage : ContentPage
    {
        private readonly UserModel usermodel;
        public CallKeyPage()
        {
            InitializeComponent();
            usermodel = new UserModel();
            this.BindingContext = GetAccessableDoors();
            RefreshDoors();
        }

        public CallKeyPage(UserModel usermodel)
        {
            InitializeComponent();
            this.usermodel = (usermodel == null) ? new UserModel(): usermodel;
            this.BindingContext = GetAccessableDoors();
            RefreshDoors();
        }

        private async void Refresh_Clicked(object send, EventArgs e)
        {
            RefreshDoors();
        }

        public void RefreshDoors()
        {
            //TODO: call api to check for new doors
            List<string> Doors = GetAccessableDoors();
            DoorContainer.Children.Clear();
            for (int i = 0; i < Doors.Count; i++)
            {
                DoorContainer.Children.Add(CreateNewDoorItem(Doors[i]));
            }
        }

        private async Task OpenDoor(string Doorname, Button button)
        {
            //TODO: fix multiple door visual bug
            //TODO: make call to open requested door.
            bool opened = false;
            int attempts = 0;
            button.Text = "Opening...";
            button.IsEnabled = false;
            while (!opened && attempts < 3)
            {
                //TODO: check if positive response message from requested door.
                //make opened true if so.
                OpeningLabel.Text = $"Opening \"{Doorname}\"";
                for (int i = 0; i <= 3; ++i)
                {
                    await Task.Run(() =>
                    {
                        return Task.Delay(1000);
                    });
                    OpeningLabel.Text += ".";
                }
                attempts++;
            }
            if (opened)
            {
                OpeningLabel.Text = $"Opened Door \"{Doorname}\"";
                button.Text = "Opened";
                await Task.Run(() => { return Task.Delay(1500); });
            }
            else
            {
                await DisplayAlert("Unable to open door", $"We were unable to open door \"{Doorname}\". Please check your internet connection and try again later.", "OK");
                OpeningLabel.Text = $"Failed to open \"{Doorname}\"";
            }
            button.Text = "Open";
            button.IsEnabled = true;
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
            button.Clicked += async (sender, args) => await OpenDoor(name, button);
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

    }
}
