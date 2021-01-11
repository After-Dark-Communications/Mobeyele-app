using Mobeye.Logic;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDevice))]
namespace Mobeye.Logic
{
    class AndroidDevice : IDevice
    {
        public string GetIdentifier()
        {
            
            //try
            //{
            //    TelephonyManager manager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);

            //    return manager.Imei;
            //}
            //catch (Exception e)
            //{
                //Console.WriteLine(e.Message);
                return "aaaa1111";
            //}
        }
    }
}
