using System;
using System.Collections.Generic;
using System.Text;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class FireBaseTest
    {
        public List<string> registrationTokens { get; set; }

        MulticastMessage message = new MulticastMessage()
        {
            Tokens = registrationTokens,
            Data = new Dictionary<string, string>() //Contains the Json format converted into a dictionary
            {
                {"devicenumber", "452" },
                {"Alertlevel","3" }
            }
        };
        public FireBaseTest()
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
            });

            registrationTokens = new List<string>()
            {
                "TOKEN-1",
                "TOKEN-2",
            };
        }
        public async Task SendResponseMessage(MulticastMessage message)
        {
            var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
            if(response.FailureCount > 0)
            {
                var failedTokens = new List<string>();
                for(var i = 0; i< response.Responses.Count; i++)
                {
                    if (!response.Responses[i].IsSuccess)
                    {
                        //The order of responses correspons to the order of the registration tokens
                        failedTokens.Add(registrationTokens[i]);
                    }
                }
            }
        }
    }
}
