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
        public UserModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public UserModel()
        {

        }
        public UserModel(string Authkey, string name, string password, string emai, string phonenumber,int permissionlevel )
        {
            this.AuthKey = AuthKey;
            this.Name = name;
            this.Password = password;
            this.Email = emai;
            this.Phonenumber = phonenumber;
            this.PermissionLevel = permissionlevel;
        }
    }
}
