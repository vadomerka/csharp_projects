using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataProcessing
{
    interface IUpdate
    {
        public event EventHandler<EventTime> Updated;
    }

    public class EventTime : EventArgs
    {
        private DateTime _time = DateTime.Now;

        public EventTime() { }

        public EventTime(DateTime userTime) 
        { 
            _time = userTime;
        }

        public DateTime Time { get { return _time; } }
    }
}
