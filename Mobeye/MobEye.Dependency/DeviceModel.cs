namespace Mobeye.Dependency
{
    public class DeviceModel
    {
        public string DeviceId { get; set; }
        public string Devicename { get; set; }
        public string CommandText { get; set; }
        public string Command { get; set; }    

        public DeviceModel ( string deviceId, string deviceName, string commandText, string command)
        {
            this.DeviceId = deviceId;
            this.Devicename = deviceName;
            this.CommandText = commandText;
            this.Command = command;
        }
    }
}
