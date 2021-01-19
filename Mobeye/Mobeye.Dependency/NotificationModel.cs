namespace Mobeye.Dependency
{
    public class NotificationModel
    {
        public string PhoneId { get; set; }
        public string UniqueMessageId { get; set; }
        public string Response { get; set; }
        public string PrivateKey { get; set; }
        //Property names are based on the POST example from Mobeye. Do not change these!
    }
}
