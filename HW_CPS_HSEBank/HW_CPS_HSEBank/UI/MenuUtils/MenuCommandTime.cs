using HW_CPS_HSEBank.Statistics;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI.MenuUtils
{
    public class MenuCommandTime : IMenuCommand
    {
        private IMenuCommand _command;
        private DateTime _dateTime;
        private double _duration;

        public MenuCommandTime(IMenuCommand command)
        {
            _dateTime = DateTime.Now;
            _command = command;
        }

        public string Type => "Statistics";
        public string Title => _command.Title;

        public DateTime StartTime => _dateTime;

        public double Duration => _duration;

        public bool Execute()
        {
            bool res = _command.Execute();
            TimeSpan dif = DateTime.Now - _dateTime;
            _duration = dif.TotalSeconds;
            var services = CompositionRoot.Services;
            services.GetRequiredService<MenuStatistics>().AddStats(this);
            return res;
        }
    }
}
