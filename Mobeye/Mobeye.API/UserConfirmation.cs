#define DEBUG
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
                    response.Dispose();
                    return user;
                }
            }
            return user;
        }
        //The call is made to the following url: https://www.api.mymobeye.com/api/auth. The url is based on the base URL provided in the APIHelper
        public string RegisterUser(string Imei, string regCode)
        {
            //Create an dynamic object to parse it to json. This is necessary for the HttpContent.
            //TODO: fix json 
            string contentString = string.Empty;

            //HttpContent regcon = new StringContent(JObject.FromObject(reg));
#if DEBUG
            using (HttpResponseMessage response = APIHelper.API.GetAsync("users?Imei=" + Imei + "&SmsKey=" + regCode).Result)
#else
            using (HttpResponseMessage response = APIHelper.API.GetAsync("api/users?Imei="+Imei+"&SmsKey="+regCode).Result)
#endif
            {
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    JArray contents = JArray.Parse(resp);
                    if (contents.Count > 0)
                    {
                        UserModel tempU = JsonToUser(response);
                        contentString = tempU.PrivateKey;
                        response.Dispose();
                        return contentString;
                    }
                }
                response.Dispose();
                return response.StatusCode.ToString();
            }
        }
        public UserModel LoginUser(string Imei, string privateKey)
        {
            UserModel user = new UserModel();
            string contentString = string.Empty;
#if DEBUG
            using (HttpResponseMessage response = APIHelper.API.GetAsync("users?Imei=" + Imei + "&PrivateKey=" + privateKey).Result)

#else
            using (HttpResponseMessage response = APIHelper.API.GetAsync("api/"users?Imei=" + Imei + "&PrivateKey=" + privateKey).Result)
#endif

            {
                if (response.IsSuccessStatusCode)
                {
                    user = JsonToUser(response);
                    response.Dispose();
                    return user;
                }
            }
            return user;
        }
        public async Task<UserModel> PortalOwnerConfirmationRequest(UserModel user)
        {
            //TODO: catch exception, if unable to connect to server/ no internet connection
            using (HttpResponseMessage response = await APIHelper.API.GetAsync(""))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    JObject obj = JObject.Parse(contents);//newtonsoft json parsing
                    UserModel res = new UserModel(obj["SmsKey"].ToString(),obj["Authcode"].ToString(),obj["name"].ToString(), obj["Imei"].ToString(), obj["Phonenumber"].ToString(),Convert.ToInt32(obj["Authlevel"]));
                    response.Dispose();
                    return res;
                }
                else
                {
                    response.Dispose();
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
                        int Auth = Convert.ToInt32(content["PermissionLevel"].ToString());

                        UserModel res = new UserModel(
                            content["SmsKey"].ToString(),
                            content["PrivateKey"].ToString(),
                            content["Name"].ToString(),
                            content["Imei"].ToString(),
                            content["Phonenumber"].ToString(),
                            Auth);//goes wrong TODO: fix 
                        return res;
                    }
                    else
                    {
                        int Auth = Convert.ToInt32(content["PermissionLevel"]);
                        UserModel res = new UserModel(
                        content["SmsKey"].ToString(),
                        content["PrivateKey"].ToString(),
                        content["Name"].ToString(),
                        content["Imei"].ToString(),
                        content["Phonenumber"].ToString(),
                        Auth);
                        return res;
                    }
                }
            }
            return null;
        }
    }
}
