
using Mobeye.Dependency;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class UserConfirmation
    {
        public async Task<bool> GetTelConfirmRequest(string tel)
        {
            throw new NotImplementedException();
        }
        public async Task<UserModel> GetCodeConfirmRequest(string code)
        {
            UserModel user = new UserModel();
            using (HttpResponseMessage response = await APIHelper.API.GetAsync("profile/?Authcode=" + code))
            {
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<UserModel>();
                    return user;
                }
            }
            return user;
        }
        public async Task<UserModel> PortalOwnerConfirmationRequest(UserModel user)
        {
            //TODO: fix deadlock
            //TODO: catch exception, if unable to connect to server/ no internet connection
            using (HttpResponseMessage response = await APIHelper.API.GetAsync(""))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    JObject obj = JObject.Parse(contents);//newtonsoft json parsing
                    UserModel res = new UserModel(obj["Authcode"].ToString(),obj["name"].ToString(),obj["Phonenumber"].ToString(),Convert.ToInt32(obj["Authlevel"]));
                    return res;
                }
                else
                {
                    return null;
                }
            }
            //try
            //{
            //    using (HttpResponseMessage response = await APIHelper.API.GetAsync("profile/?emailaddress=" + user.Email))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        { 
            //            UserModel receivedUser = response.Content.ReadAsAsync<UserModel>().Result;
            //            return receivedUser;
            //        }
            //    }
            //
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return null;
        }
    }
}
