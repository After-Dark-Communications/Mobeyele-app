using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class UserModel
    {
        public string name { get; set; }
        public string password { get; set; }
        public string emailaddress { get; set; }
        public int Authlevel { get; set; }
        public string Authcode { get; set; }
        public string Accescode { get; set; }
        public string Phonenumber { get; set; }
        public List<DeviceModel> ExternalDevices { get; set; }
        public UserModel(string email, string password)
        {
            this.emailaddress = email;
            this.password = password;
        }
        public UserModel()
        {

        }
        public UserModel(string name, string password, string emailaddress, int Authlevel, string Authcode, string Accescode,string Phonenumber = null)
        {
            this.name=name;
            this.password=password;
            this.emailaddress=emailaddress;
            this.Authlevel=Authlevel;
            this.Authcode=Authcode;
            this.Accescode=Accescode;
            this.Phonenumber = Phonenumber;
        }
    }
}