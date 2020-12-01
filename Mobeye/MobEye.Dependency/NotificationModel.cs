using System;
using System.Collections.Generic;
using System.Text;

namespace Mobeye.Dependency
{
    public class NotificationModel
    {
        string Devicename { get; set; }
        string Location { get; set; }
        string Alarmtext { get; set; }
        string SetReset { get; set; }
        int Priority { get; set; }
        DateTime TimeOfAlarm { get; set; }
        string Value { get; set; }
        string MessageID { get; set; }
        string[] Recipients { get; set; }
        bool Escalation { get; set; }
    }
}
