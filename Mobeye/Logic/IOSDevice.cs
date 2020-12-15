using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Logic
{
    public class IOSDevice : IDevice
    {
        public string GetIdentifier()
        {
            //var nsUid = UIDevice.CurrentDevice.IdentifierForVendor;
            //string guidElements = nsUid.AsString();

            //return guidElements;
            return "000000000000000";
        }
    }
}
