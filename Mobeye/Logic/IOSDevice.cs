namespace Mobeye.Logic
{
    public class IosDevice : IDevice
    {
        public string GetIdentifier(string smscode)
        {
            //var nsUid = UIDevice.CurrentDevice.IdentifierForVendor;
            //string guidElements = nsUid.AsString();

            //return guidElements;
            return "000000000000000";
        }
    }
}
