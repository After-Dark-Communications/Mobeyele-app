using System;
using Mobeye.Dependency;
using Mobeye.API;

namespace Mobeye.Logic
{
	public class Notification
	{
		public string ID { get; set; }
		public string Message { get; set; }

		private NotificationHandling _notificationHandler;

		public Notification()
        {
			_notificationHandler = new NotificationHandling();
        }

		public void PushToPhone(NotificationModel notification)
		{
			ID = notification.MessageID;
			Message = notification.Alarmtext;
			if(notification.Priority > 0)
            {
				//alarm
				//push notification
            }
            else
            {
				//alert
				//push notification
            }
		}

		public void ConfirmedNotification(string messageID, string identifier, bool status, string privateKey)
		{
			//_notificationHandler.ConfirmedNotification(messageID, identifier, status, privateKey);
			throw new NotImplementedException();
		}
	}
}