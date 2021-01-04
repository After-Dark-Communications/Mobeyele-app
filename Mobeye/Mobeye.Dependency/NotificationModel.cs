namespace Mobeye.Dependency
{
    public class NotificationModel
    {
        public string Authorization { get; set; }
        public string Devicename { get; set; }
        public string Devicelocation { get; set; }
        public string Alarmtext { get; set; }
        public string Value { get; set; }
        //Property names are based on the POST example from Mobeye. Do not change these!
    }
}
