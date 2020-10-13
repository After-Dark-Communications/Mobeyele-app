using System;

public class Notification
{
	private int ID { get; set; }
	private string Message { get; set; }

	public void PushToDevice(NotificationModel notification)
	{
		throw new NotImplementedException();
	}

	public bool ConfirmNotification(NotificationModel notification)
	{
		throw new NotImplementedException();
	}
}