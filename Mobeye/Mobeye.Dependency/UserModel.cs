using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class UserModel
    {
        public string AuthKey { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public List<DeviceModel> ExternalDevices { get; set; }
        public int PermissionLevel { get; set; }
    }
}
