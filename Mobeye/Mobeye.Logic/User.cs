using System;
using Mobeye.Dependency;

namespace Mobeye.Logic
{
    public class User
    {
        public UserModel LogIn(string username, string password)
        {
            //pass username + password to API
            //API returns UserModel or null
            //return UserModel
            throw new NotImplementedException();
        }
        public bool LogIn(string accessCode)
        {
            throw new NotImplementedException();
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