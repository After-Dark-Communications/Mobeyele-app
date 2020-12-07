using Mobeye.Dependency;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class ExternalDeviceFunctions
    {
        public async Task<List<int>> GetUserDevicesId(UserModel user)
        {
            List<int> ids = new List<int>();
            using (HttpResponseMessage response = await APIHelper.API.GetAsync("" + user.Imei + user.PrivateKey))
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
            using (HttpResponseMessage response = await APIHelper.API.GetAsync(""))
            {
                if(response.IsSuccessStatusCode)
                {
                    devices = response.Content.ReadAsAsync<List<DeviceModel>>().Result;
                    return devices;
                }
                return devices;
            }
        }
        //GetUserDevices(GetUserDevicesID(user))
    }
}
