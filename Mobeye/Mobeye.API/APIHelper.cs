﻿using System;
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
            string url = "https://www.api.mymobeye.com/api";
            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            Api = new HttpClient();
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