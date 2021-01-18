using System.Collections.Generic;

namespace Mobeye.Dependency
{
    public class UserModel
    {
        public string UserRole { get; set; }
        public string PrivateKey { get; set; }
        public DeviceModel[] Devices { get; set; }
      
        //public List<DeviceModel> ExternalDevices { get; set; }
       
        public UserModel()
        {

        }
        public UserModel(string userRole, string privatekey, DeviceModel[] devices)
        {
            this.UserRole = userRole;
            this.PrivateKey = privatekey;
            this.Devices = devices;
        }
    }
}
