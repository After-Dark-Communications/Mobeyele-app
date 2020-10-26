using Mobeye.Dependency;
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

        public async Task<string> GetCodeConfirmRequest(string code)
        {
            using (HttpResponseMessage response = await APIHelper.API.GetAsync("")) //API Call to Mobeye
            {
                if(response.IsSuccessStatusCode)
                {
                    string receivedcode = await response.Content.ReadAsStringAsync();
                    return receivedcode;
                }
            }
            return null;
        }
        public async Task<UserModel> PortalOwnerConfirmationRequest(UserModel user)
        {
            using(HttpResponseMessage response = await APIHelper.API.GetAsync(""))
            {
                if(response.IsSuccessStatusCode)
                {
                    UserModel receivedUser = await response.Content.ReadAsAsync<UserModel>();
                    return receivedUser;
                }
            }
            return null;
        }
    }
}
