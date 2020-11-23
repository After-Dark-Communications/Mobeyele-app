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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Attempts to login the user with the provided access code
        /// </summary>
        /// <param name="accessCode">The access code filled in by the user</param>
        /// <returns>A Usermodel</returns>
        public UserModel LogInWithAccessCode(string accessCode)
        {
            //pass accessCode to API
            //API returns UserModel or null
            //return UserModel

            //No longer applicable since the access code doesn't require a usermodel
            //UserModel user = new UserModel();
            //user = _user.GetCodeConfirmRequest(accessCode);
            //return user;
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