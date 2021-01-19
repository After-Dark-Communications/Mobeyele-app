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

            try
            {
                var response = ApiHelper.Api.PostAsync("https://www.api.mymobeye.com/api/registerphone", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;

                    string[] partOne = resp.Split(':');
                    string[] partTwo = partOne[1].Split('"');
                    string privatekey = partTwo[1];


                    return privatekey;

                }
                //return response.StatusCode.ToString();
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
            using (HttpResponseMessage response = ApiHelper.Api.PostAsync("https://www.api.mymobeye.com/api/phoneauthorization", byteContent).Result)
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
        public bool CreateAuthorizationCode(string code, string privatekey)
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
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync(""))
            {
                if (response.IsSuccessStatusCode)
                {
                    Task<string> resp = response.Content.ReadAsStringAsync();
                    string contents = resp.Result;
                    JObject obj = JObject.Parse(contents);
                    //newtonsoft json parsing
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

        }
        private UserModel JsonToUser(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string resp = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UserModel>(resp);


                /*JArray contents = JArray.Parse(resp);

                if (contents.Count > 0)//json always returns something if the request is valid, thus it can return an empty array
                {
                    JObject content = JObject.FromObject(contents[0]);
                    DeviceModel[] newArray = new DeviceModel[JArray.Parse(content["Devices"])];
                    foreach (var device in content["Devices"])
                    {
                        DeviceModel newDevice = new DeviceModel(
                            device["DeviceId"]?.ToString(),
                            device["DeviceName"]?.ToString(),
                            device["Command"]?.ToString(),
                            device["CommmandText"]?.ToString());

                        newArray[newArray.Length] = newDevice;                        
                    }
                    
#if DEBUG
                    UserModel res = new UserModel(
                        content["UserRole"]?.ToString(),
                        content["PrivateKey"]?.ToString(),
                        content[""]?);
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
                }*/
            }
            return null;
        }
    }
}
