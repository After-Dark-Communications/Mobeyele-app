using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class UserConfirmation
    {
        
        public string GottenCode { get; set; }
        public string GottenTel { get; set; }

        public bool SendTelConfirmRequest(string tel)
        {
            return false;
        }
        public async Task<bool> SendCodeConfirmRequest(string code)
        {
            return false;
        }
        public bool PortalOwnerConfirmationRequest(string username, string password)
        {
            return false;
        }
    }
}
