using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Mobeye.API
{
    public static class APIHelper
    {
        public static HttpClient API { get; set; }

        public static void InitaliazeClient(string RuntimePlatform)
        {
            API = new HttpClient();
            //API.BaseAddress = new Uri("http://localhost:3000/");
            API.BaseAddress = new Uri("https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/");
            API.DefaultRequestHeaders.Accept.Clear();
            API.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}