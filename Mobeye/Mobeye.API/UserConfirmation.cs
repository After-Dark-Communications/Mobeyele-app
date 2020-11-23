
using Mobeye.Dependency;
using Newtonsoft.Json;
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
        //The call is made to the following url: https://www.api.mymobeye.com/api/auth. The url is based on the base URL provided in the APIHelper
        public async Task<string> AuthorizeUser(string Imei, string regCode)
        {
            //Create an dynamic object to parse it to json. This is necessary for the HttpContent.
            var contentString = string.Empty;
            dynamic reg = new JObject();
            reg.Imei = Imei;
            reg.regCode = regCode;

            HttpContent regcon = new StringContent(JObject.FromObject(reg));
            using (HttpResponseMessage response = await APIHelper.API.PostAsync("api/auth/", regcon))
            {
                if (response.IsSuccessStatusCode)
                {
                    contentString = response.Content.ReadAsStringAsync().Result;
                    return contentString;
                }
                return response.StatusCode.ToString();
            }
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
                    UserModel res = new UserModel(obj["SmsKey"].ToString(),obj["Authcode"].ToString(),obj["name"].ToString(),obj["Phonenumber"].ToString(),Convert.ToInt32(obj["Authlevel"]));
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
        private UserModel JsonToUser(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                JArray contents = JArray.Parse(resp);
                if (contents.Count > 0)//json always returns something if the request is valid, thus it can return an empty array
                {
                    JObject content = JObject.FromObject(contents[0]);
                    if (content["Phonenumber"] != null)
                    {
                        UserModel res = new UserModel(
                            content["SmsKey"].ToString(),
                            content["PrivateKey"].ToString(),
                            content["Name"].ToString(),
                            content["Phonenumber"].ToString(),
                            Convert.ToInt32(content["Authlevel"]));
                        return res;
                    }
                    else
                    {
                        UserModel res = new UserModel(
                        content["SmsKey"].ToString(),
                        content["PrivateKey"].ToString(),
                        content["Name"].ToString(),
                        content["Phonenumber"].ToString(),
                        Convert.ToInt32(content["Authlevel"]));
                        return res;
                    }
                }
            }
            return null;
        }
    }
}
