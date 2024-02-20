using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataProcessing.EventProcessing
{
    /// <summary>
    /// Интерфейс для всех объектов, которые могут обновляться.
    /// </summary>
    public interface IUpdate
    {
        public event EventHandler<EventTime> Updated;
    }

    /// <summary>
    /// Наследник EventArgs. Хранит дату и время.
    /// </summary>
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
