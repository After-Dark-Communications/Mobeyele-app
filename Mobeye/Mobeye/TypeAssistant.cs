using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mobeye
{
    class TypeAssistant
    {
        public event EventHandler idled = delegate { };
        public int waitingMilliSeconds { get; set; }
        Timer waitingTimer;
        public TypeAssistant(int milliSeconds = 600)
        {
            waitingMilliSeconds = milliSeconds;
            waitingTimer = new Timer(p => { idled(this, EventArgs.Empty); });
        }

        public void TextChanged()
        {
            waitingTimer.Change(waitingMilliSeconds, Timeout.Infinite);
        }
    }
}
