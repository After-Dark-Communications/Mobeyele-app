using Mobeye.Dependency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class UserConfirmation
    {
#if DEBUG
         private readonly string Url = "https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/";
#else 
         private readonly string Url = "https://www.api.mymobeye.com/api";
#endif
        public UserConfirmation()
        {

        }
        public async Task<UserModel> GetCodeConfirmRequest(string code)
        {
            UserModel user = new UserModel();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync("profile/?Authcode=" + code))
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
        public string RegisterUser(string imei, string smsCode)
        {

            var data = new
            {
                PhoneId = imei,
                Code = smsCode
            };
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            Console.WriteLine(myContent);

            try
            {
                var response = ApiHelper.Api.PostAsync($"{Url}/registerphone", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;

                    string[] partOne = resp.Split(':');
                    string[] partTwo = partOne[1].Split('"');
                    string privatekey = partTwo[1];

                    return privatekey;

                }
                return null;
            }
            catch (Exception e)

            {

            }
            return "nothing";

        }

        public UserModel LoginUser(string imei, string privateKey)
        {
            var data = new
            {
                PhoneId = imei,
                privateKey = privateKey
            };
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //   try{
            using (HttpResponseMessage response = ApiHelper.Api.PostAsync($"{Url}/phoneauthorization", byteContent).Result)
            {
                Console.WriteLine(response);
                return JsonToUser(response);
            }
            //    }
            //    catch(Exception e)
            //    { 
            //        UserModel temp = new UserModel();
            //        return temp;
            //    }

        }
        /*public bool CreateAuthorizationCode(string code, string privatekey)
        {
            HttpContent authcode = new StringContent("code");

            using (HttpResponseMessage response = ApiHelper.Api.PostAsync("users?Privatekey=" + privatekey, authcode).Result)
            {
                return response.IsSuccessStatusCode;
            }
        }

       public async Task<List<DeviceModel>> GetAuthorization(string imei, string privatekey)
        {
            List<DeviceModel> devices = new List<DeviceModel>();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync($"users?Imei={imei}&PrivateKey={privatekey}"))
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
                                                          // UserModel res = new UserModel(obj["SmsKey"]?.ToString(), obj["Authcode"]?.ToString(), obj["name"]?.ToString(), obj["Imei"]?.ToString(), obj["Phonenumber"]?.ToString(), Convert.ToInt32(obj["Authlevel"]));
                                                          // return res;
                    return null;
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

        }*/
        private UserModel JsonToUser(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UserModel>(resp);
            }
            return null;
        }
    }
}
