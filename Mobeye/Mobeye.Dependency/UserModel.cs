using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class UserModel
    {
        public string SmsKey { get; set; }
        public string PrivateKey { get; set; }
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Phonenumber { get; set; }
        public List<DeviceModel> ExternalDevices { get; set; }
        public int PermissionLevel { get; set; }
        public UserModel()
        {

        }
        public UserModel(string smskey, string privatekey, string name,string Imei, string phonenumber,int permissionlevel)
        {
            this.SmsKey = smskey;
            this.PrivateKey = privatekey;
            this.Name = name;
            this.Imei = Imei;
            this.Phonenumber = phonenumber;
            this.PermissionLevel = permissionlevel;
        }
    }
}
