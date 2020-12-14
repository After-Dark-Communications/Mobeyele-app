using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class NotificationModel
    {
        public string Devicename { get; set; }
        public string Location { get; set; }
        public string Alarmtext { get; set; }
        public string SetReset { get; set; }
        public int Priority { get; set; }
        public DateTime TimeOfAlarm { get; set; }
        public string Value { get; set; }
        public string MessageID { get; set; }
        public string[] Recipients { get; set; }
        public bool Escalation { get; set; }
    }
}
