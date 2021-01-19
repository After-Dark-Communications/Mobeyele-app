using Mobeye.Dependency;
using Newtonsoft.Json;
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
            string Url = "https://onesignal.com/api/v1/notifications";
            string msg = "";
            var data = new
            {
                PhoneId = notification.PhoneId,
                UniqueMessageId = notification.UniqueMessageId,
                Response = notification.Response,
                Privatekey = notification.PrivateKey
            };
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage response = ApiHelper.Api.PostAsync(Url, byteContent).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    msg = "Notification sent!";
                }
                else
                {
                    msg = "Notification failed to send";
                }
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
