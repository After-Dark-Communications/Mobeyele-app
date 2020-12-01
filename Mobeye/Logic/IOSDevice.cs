using System;
using System.Collections.Generic;
using System.Text;
using UIKit;//TODO: fix could not find error

namespace Mobeye.Logic
{
    public class IOSDevice : IDevice
    {
        public string GetIdentifier()
        {
#if DEBUG
            return "000000000000000";
#else
var nsUid = UIDevice.CurrentDevice.IdentifierForVendor;
            string guidElements = nsUid.AsString();

            return guidElements;
#endif
        }
    }
}
