using System;

public class Notification
{
    private int ID { get; set; }
    private string Message { get; set; }

    public void PushToDevice(Notification notification)
    {
        throw new NotImplementedException();
    }

    public bool ConfirmNotification(Notification notification)
    {
        throw new NotImplementedException();
    }
}