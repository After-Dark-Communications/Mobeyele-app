using System;
using Mobeye.Dependency;

namespace Mobeye.Logic
{
	public class Notification
	{
		public string ID { get; set; }
		public string Message { get; set; }

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

		public bool ConfirmedNotification(NotificationModel notification)
		{
			throw new NotImplementedException();
		}
	}
}