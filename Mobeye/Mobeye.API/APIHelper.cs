using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Mobeye.API
{
    public static class ApiHelper
    {
        public static HttpClient Api { get; set; }

        public static void InitaliazeClient(string runtimePlatform)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            Api = new HttpClient(handler);
            Api.DefaultRequestHeaders.Accept.Clear();
            Api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}