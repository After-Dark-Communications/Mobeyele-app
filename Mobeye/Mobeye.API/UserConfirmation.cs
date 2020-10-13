using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.API
{
    public class UserConfirmation
    {
        public string GottenCode { get; set; }
        public string GottenTel { get; set; }

        public bool SendTelConfirmRequest(string Tel)
        {
            return false;
        }
        public bool SendCodeConfirmRequest(string Code)
        {
            return false;
        }
        public bool PortalOwnerConfirmationRequest(string Username, string Password)
        {
            return false;
        }
    }
}
