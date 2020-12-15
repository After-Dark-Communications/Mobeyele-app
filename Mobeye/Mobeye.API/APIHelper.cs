using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Mobeye.API
{
    public static class ApiHelper
    {
        public static HttpClient Api { get; set; }

        public static void InitaliazeClient(string runtimePlatform)
        {
            Api = new HttpClient();
#if DEBUG
            Api.BaseAddress = new Uri("https://my-json-server.typicode.com/Irishmun/mobeyeletestdb/");
#else 
            API.BaseAddress = new Uri("https://www.api.mymobeye.com/api");
#endif
            Api.DefaultRequestHeaders.Accept.Clear();
            Api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}