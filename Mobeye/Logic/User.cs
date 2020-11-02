using System;
using Mobeye.Dependency;
using Mobeye.API;
using System.Threading.Tasks;

namespace Mobeye.Logic
{
    public class User
    {
        private readonly UserConfirmation _user;
        public User()
        {
            _user = new UserConfirmation();
        }
        public User(UserConfirmation user)
        {
            _user = user;
        }
        public UserModel LogInWithCredentials(string email, string password)
        {
            //pass username + password to API
            //API returns UserModel or null
            //return UserModel
            UserModel user = new UserModel(email, password);
            UserModel receiveduser = _user.PortalOwnerConfirmationRequest(user).Result;
            return receiveduser;
        }
        public UserModel LogInWithAccessCode(string accessCode)
        {
            //pass accessCode to API
            //API returns UserModel or null
            //return UserModel
            UserModel user = new UserModel();
            user = _user.GetCodeConfirmRequest(accessCode).Result;
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