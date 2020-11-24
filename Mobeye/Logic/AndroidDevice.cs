using System;
using System.Collections.Generic;
using System.Text;
using Android.Telephony;//TODO: fix could not find error
using Mobeye.Logic;
using Xamarin.Forms;

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
            catch
            {
                return null;
            }
            return "";
        }
    }
}
