
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
        public UserModel GetCodeConfirmRequest(string code)
        {
            using (HttpResponseMessage response = APIHelper.API.GetAsync("profile/?Authcode=" + code).Result)
            {
                return JsonToUser(response);
            }
        }
        public UserModel PortalOwnerConfirmationRequest(UserModel user)
        {
            //TODO: catch exception, if unable to connect to server/ no internet connection
            using (HttpResponseMessage response = APIHelper.API.GetAsync("profile/?emailaddress=" + user.emailaddress).Result)
            {
                return JsonToUser(response);
            }
            #region old
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
            #endregion
        }
        private UserModel JsonToUser(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                JArray contents = JArray.Parse(resp);
                if (contents.Count > 0)//json always returns something if the request is valid, thus it can return an empty array
                {
                    JObject content = JObject.FromObject(contents[0]);
                    if (content["Phonenumber"]!= null)
                    {
                    UserModel res = new UserModel(
                        content["name"].ToString(),
                        content["password"].ToString(),
                        content["emailaddress"].ToString(),
                        Convert.ToInt32(content["Authlevel"]),
                        content["Authcode"].ToString(),
                        content["Accescode"].ToString(),
                        content["Phonenumber"].ToString());
                    return res;
                    }
                    else
                    {
                        UserModel res = new UserModel(
                        content["name"].ToString(),
                        content["password"].ToString(),
                        content["emailaddress"].ToString(),
                        Convert.ToInt32(content["Authlevel"]),
                        content["Authcode"].ToString(),
                        content["Accescode"].ToString());
                        return res;
                    }
                }
            }
            return null;
        }
    }
}