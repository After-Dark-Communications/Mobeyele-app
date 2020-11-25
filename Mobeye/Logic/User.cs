using System;
using Mobeye.Dependency;
using Mobeye.API;
using System.Threading.Tasks;

namespace Mobeye.Logic
{
    public class User
    {
        private readonly UserConfirmation _user;
        private readonly IDevice _device;
        public User()
        {
            _user = new UserConfirmation();
            _device = new AndroidDevice();
        }
        public User(string vendor)
        {
            _user = new UserConfirmation();
            switch (vendor){
                case "Android":
                    _device = new AndroidDevice();
                    break;
                case "iOS":
                    _device = new IOSDevice();
                    break;
                default:
                    //nothing
                    break;
            }
        }
        public User(UserConfirmation user)
        {
            _user = user;
        }
        public string Register(string smsCode)
        {
            string privateKey = "";
            if(_device != null)
            {
                privateKey = _user.RegisterUser(_device.GetIdentifier(), smsCode).Result;
            }
            return privateKey;
        }
        public UserModel LogInWithPrivateKey(string privateKey)
        {
            //pass username + password to API
            //API returns UserModel or null
            //return UserModel
            UserModel user = _user.LoginUser(privateKey, _device.GetIdentifier()).Result;

            if(user != null)
            {
                return user;
            }
            return null;
        }
        
        /*public bool LogIn(string authorizationCode)
        {
            throw new NotImplementedException();
        }*/
        public bool LogOut()
        {
            throw new NotImplementedException();
        }
        public void RedirectTo(int perms)
        {
            throw new NotImplementedException();
        }
    }
}