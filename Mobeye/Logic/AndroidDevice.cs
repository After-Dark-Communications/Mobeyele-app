using System;
using System.Collections.Generic;
using System.Text;
using Android.Telephony;
using Mobeye.Logic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(AndroidDevice))]
namespace Mobeye.Logic
{
    class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
            
            try
            {
                TelephonyManager manager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);

                return manager.Imei;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
