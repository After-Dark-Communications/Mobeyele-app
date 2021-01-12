using Mobeye.Dependency;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class UserConfirmation
    {
        private string url;

        public UserConfirmation()
        {
#if DEBUG
            url = "https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/";
#else 
            url = "https://www.api.mymobeye.com/api";
#endif
        }

        public async Task<UserModel> GetCodeConfirmRequest(string code)
        {
            UserModel user = new UserModel();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync(url + "profile/?Authcode=" + code))
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
        public string RegisterUser(string imei, string regCode)
        {
            //Create an dynamic object to parse it to json. This is necessary for the HttpContent.
            //TODO: fix json 
            string contentString;

            //HttpContent regcon = new StringContent(JObject.FromObject(reg));
            using (HttpResponseMessage response = ApiHelper.Api.GetAsync(url + "users?Imei=" + imei + "&SmsKey=" + regCode).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    JArray contents = JArray.Parse(resp);
                    if (contents.Count > 0)
                    {
                        contentString = (string)contents[0]["PrivateKey"];
                        return contentString;
                    }
                }
                return response.StatusCode.ToString();
            }
        }
        public UserModel LoginUser(string privateKey, string imei)
        {
            using (HttpResponseMessage response = ApiHelper.Api.GetAsync(url + "users?Imei=" + imei + "&PrivateKey=" + privateKey).Result)
            {
                return JsonToUser(response);
            }
        }
        public bool CreateAuthorizationCode(string code, string privatekey)
        {
            HttpContent authcode = new StringContent("code");

            using (HttpResponseMessage response = ApiHelper.Api.PostAsync(url + "users?Privatekey=" + privatekey, authcode).Result)
            {
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<List<DeviceModel>> GetAuthorization(string imei, string privatekey)
        {
            List<DeviceModel> devices = new List<DeviceModel>();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync(url + $"users?Imei={imei}&PrivateKey={privatekey}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    devices = response.Content.ReadAsAsync<List<DeviceModel>>().Result;
                    return devices;
                }
                return devices;
            }
        }
        public async Task<UserModel> PortalOwnerConfirmationRequest(UserModel user)
        {
            //TODO: fix deadlock
            //TODO: catch exception, if unable to connect to server/ no internet connection
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync(""))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    JObject obj = JObject.Parse(contents);//newtonsoft json parsing
                    UserModel res = new UserModel(obj["SmsKey"]?.ToString(), obj["Authcode"]?.ToString(), obj["name"]?.ToString(), obj["Imei"]?.ToString(), obj["Phonenumber"]?.ToString(), Convert.ToInt32(obj["Authlevel"]));
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
#if DEBUG
                    UserModel res = new UserModel(
                        content["SmsKey"]?.ToString(),
                        content["PrivateKey"]?.ToString(),
                        content["Name"]?.ToString(),
                        content["Imei"]?.ToString(),
                        content["Phonenumber"].ToString(),
                        Convert.ToInt32(content["PermissionLevel"]));
                    return res;
#else
                    UserModel res = new UserModel(
                        content["Code"]?.ToString(),
                        content["privateKey"]?.ToString(),
                        content["Name"]?.ToString(),
                        content["phoneId"]?.ToString(),
                        content["Phonenumber"].ToString(),
                        Convert.ToInt32(content["UserRole"]));
                    return res;
#endif
                }
            }
            return null;
        }
    }
}
