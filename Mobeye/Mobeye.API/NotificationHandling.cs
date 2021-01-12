using Mobeye.Dependency;
using System.Net.Http;
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
            HttpResponseMessage response = await ApiHelper.Api.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                notification = await response.Content.ReadAsAsync<NotificationModel>();
            }
            return notification;
        }
        public string SendNotification(NotificationModel notification)
        {
            string msg = "";
            HttpResponseMessage response = ApiHelper.Api.PostAsync();
            if(response.IsSuccessStatusCode)
            {
                msg = "Notification sent!";
            }
            else
            {
                msg = "Notification failed to send";
            }
            return msg;
        }
        public void ConvertJsonToNotification(string json)
        {

        }
        public void ConfirmedNotification(bool confirmed)
        {

        }
    }
}
