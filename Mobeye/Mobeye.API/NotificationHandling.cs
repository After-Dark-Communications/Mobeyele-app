using System;
using System.IO;
using Mobeye.Dependency; 

namespace Mobeye.API
{
    public class NotificationHandling
    {
        public int Id { get; set; }
        public string Message { get; set; }


        public NotificationModel ReceiveNotification()
        {
            return null;
        }
        public NotificationModel ConvertJsonToNotification()
        {
            return null;
        }
        public void ConfirmedNotification(bool confirmed, NotificationModel notification)
        {
            
        }
    }
}
