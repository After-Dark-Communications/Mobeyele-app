using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class NotificationModel
    {
        public string Authorization { get; set; }
        public string devicename { get; set; }
        public string devicelocation { get; set; }
        public string alarmtext { get; set; }
        public string value { get; set; }
        //Property names are based on the POST example from Mobeye. Do not change these!
    }
}
