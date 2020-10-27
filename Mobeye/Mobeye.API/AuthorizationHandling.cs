using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class AuthorizationHandling
    {
        public async Task<bool> StoreAuthCode(string authCode)
        {
            HttpResponseMessage response = await APIHelper.API.PostAsJsonAsync("", authCode);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false; 
        }
    }
}
