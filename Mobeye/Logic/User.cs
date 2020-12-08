﻿using System;
using Mobeye.Dependency;
using Mobeye.API;
using System.Threading.Tasks;

namespace Mobeye.Logic
{
    public class User
    {
        private UserConfirmation _user;
        private readonly IDevice _device;
        public User()
        {
            _user = new UserConfirmation();
            _device = new AndroidDevice();
        }
        public User(string vendor)
        {
            _user = new UserConfirmation();
            switch (vendor)
            {
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
            if (_device != null)
            {
                privateKey = _user.RegisterUser(_device.GetIdentifier(), smsCode);
            }
            return privateKey;
        }
        public UserModel LogInWithPrivateKey(string privateKey)
        {
            //pass username + password to API
            //API returns UserModel or null
            //return UserModel
            _user = _user==null ? new UserConfirmation() : _user;
            UserModel user = _user.LoginUser(privateKey, _device.GetIdentifier());

            if (user != null)
            {
                return user;
            }
            return null;
        }
        public UserModel createMinimalUM(string smsKey, string privateKey)
        {
            UserModel user = new UserModel();
            user.SmsKey = smsKey;
            user.PrivateKey = privateKey;
            user.Imei = _device.GetIdentifier();
            user.Name = "";
            user.Phonenumber = "";
            user.PermissionLevel = 999;
            return user;
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