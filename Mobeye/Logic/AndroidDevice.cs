using Mobeye.Logic;
using Xamarin.Forms;

namespace Mobeye.Logic
{
    class AndroidDevice : IDevice
    {
        public string GetIdentifier(string smscode)
        {
            //try
            //{
            //    TelephonyManager manager = (TelephonyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.TelephonyService);

            //    return manager.Imei;
            //}
            //catch (Exception e)
            //{
            //Console.WriteLine(e.Message);
            //}

            switch (smscode)
            {
                case "11111":
                    return "aaaa1111";

                case "22222":
                    return "bbbb2222";

                case "33333":
                    return "cccc3333";

                case "44444":
                    return "dddd4444";

                case "55555":
                    return "eeee5555";

                default:
                    return "000000000000";

            }
            
            //return "aaaa1111";

        }
    }
}
