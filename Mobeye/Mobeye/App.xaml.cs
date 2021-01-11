using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Mobeye.API;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Collections.Generic;

namespace Mobeye
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApiHelper.InitaliazeClient(Device.RuntimePlatform);
            MainPage = new NavigationPage(new MainPage());

            //Remove this method to stop OneSignal Debugging  
            OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);

            OneSignal.Current.StartInit("4426fab6-1d56-4a81-8224-d62d61f0ceb6")
            .Settings(new Dictionary<string, bool>() {
            { IOSSettings.kOSSettingsKeyAutoPrompt, false },
             { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
            .InFocusDisplaying(OSInFocusDisplayOption.Notification)
            .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
