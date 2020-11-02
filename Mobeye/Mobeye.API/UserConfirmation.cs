
﻿using Mobeye.Dependency;
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
            using (HttpResponseMessage response = await APIHelper.API.GetAsync(""))
            {
                if(response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<UserModel>();
                    return user;
                }
            }
            return user;
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
