using System.Net.Http;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class AuthorizationHandling
    {
        public async Task<bool> StoreAuthCode(string authCode)
        {
            HttpResponseMessage response = await ApiHelper.Api.PostAsJsonAsync("", authCode);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false; 
        }
    }
}
