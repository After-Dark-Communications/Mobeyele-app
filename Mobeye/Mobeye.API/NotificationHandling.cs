using Mobeye.Dependency;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mobeye.API
{
    public class NotificationHandling
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public async Task<NotificationModel> ReceiveNotification(string path)
        {
            NotificationModel notification = null;
            HttpResponseMessage response = await APIHelper.API.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                notification = await response.Content.ReadAsAsync<NotificationModel>();
                response.Dispose();
            }
            return notification;
        }
        public void ConvertJsonToNotification(string json)
        {

        }
        public void ConfirmedNotification(bool confirmed)
        {

        }
    }
}
