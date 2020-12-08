using Mobeye.Dependency;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mobeye.API
{
    public class ExternalDeviceFunctions
    {
        public async Task<List<int>> GetUserDevicesId(UserModel user)
        {
            List<int> ids = new List<int>();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync("" + user.Imei + user.PrivateKey))
            {
                if (response.IsSuccessStatusCode)
                {
                    ids = response.Content.ReadAsAsync<List<int>>().Result;
                    return ids;
                }
            }

            return ids;
        }

        public async Task<List<DeviceModel>> GetUserDevicesTest(List<int> ids)
        {
            List<DeviceModel> devices = new List<DeviceModel>();
            using (HttpResponseMessage response = await ApiHelper.Api.GetAsync(""))
            {
                if (response.IsSuccessStatusCode)
                {
                    devices = response.Content.ReadAsAsync<List<DeviceModel>>().Result;
                    return devices;
                }

                return devices;
            }
        }

        public async Task<String> OpenDoor(UserModel model, int deviceid, string command)
        {
            string contentString;
            dynamic device = new JObject();
            device.deviceid = deviceid;
            device.command = command;

            HttpContent deviceContent = new StringContent(JObject.FromObject(device));
            using (HttpResponseMessage response = await ApiHelper.Api.PostAsync("api/auth/", deviceContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    contentString = response.Content.ReadAsStringAsync().Result;
                    return contentString;
                }

                return response.StatusCode.ToString();
            }

            //GetUserDevices(GetUserDevicesID(user))
        }
    }
}
