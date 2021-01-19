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
            /* var EndPoint = "https://www.api.mymobeye.com/api";
             var httpClientHandler = new HttpClientHandler();
             httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
             {
                 return true;
             };
             var httpClient = new HttpClient(httpClientHandler) { BaseAddress = new Uri(EndPoint) };*/

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            Api = new HttpClient(handler);
#if DEBUG
            Api.BaseAddress = new Uri("https://www.api.mymobeye.com/api");
#else 
            Api.BaseAddress = new Uri("https://www.api.mymobeye.com/api");
#endif
            Api.DefaultRequestHeaders.Accept.Clear();
            Api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}