using System;
using Mobeye.Dependency;

namespace Mobeye.Logic
{
	public class Notification
	{
		private int ID { get; set; }
		private string Message { get; set; }

		public void PushToPhone(NotificationModel notification)
		{
			throw new NotImplementedException();
		}

		public bool ConfirmedNotification(NotificationModel notification)
		{
			throw new NotImplementedException();
		}
	}
}