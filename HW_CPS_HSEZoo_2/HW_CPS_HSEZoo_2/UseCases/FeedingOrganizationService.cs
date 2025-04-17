using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Entities.Events;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using System.Timers;

namespace HW_CPS_HSEZoo_2.UseCases
{
    internal class FeedingOrganizationService
    {
        public event ZooEvents.FeedingTimeEvent? feedEvent;

        private System.Timers.Timer _timer = new System.Timers.Timer();
        private IServiceProvider services = CompositionRoot.Services;

        public List<ISchedule> GetSchedules()
        {
            var rep = services.GetRequiredService<FeedingTimeRepository>();
            return rep.GetEntities();
        }

        public ISchedule GetSchedule(int id)
        {
            var rep = services.GetRequiredService<FeedingTimeRepository>();
            return rep.GetEntity(id);
        }

        public ISchedule Create(Animal animal, FeedingScheduleDTO dto)
        {
            return FeedingScheduleFactory.Create(animal, dto);
        }

        public void AddSchedule(ISchedule enclosure)
        {
            var rep = services.GetRequiredService<FeedingTimeRepository>();
            rep.AddEntity(enclosure);
            TimerUpdate();
        }

        public void RemoveSchedule(ISchedule enclosure)
        {
            var rep = services.GetRequiredService<FeedingTimeRepository>();
            rep.RemoveEntity(enclosure);
            TimerUpdate();
        }

        private void TimerUpdate()
        {
            var _entities = services.GetRequiredService<FeedingTimeRepository>().GetEntities();
            _entities.Sort((ISchedule a, ISchedule b) => a.Time.CompareTo(b.Time));

            if (_entities.Count > 0 && _entities[0].Time >= DateTime.UtcNow)
            {
                double when = (_entities[0].Time - DateTime.UtcNow).TotalMilliseconds;
                _timer = new System.Timers.Timer(when);
                _timer.Elapsed += OnTimedEvent;
                _timer.Start();
            } else if (_entities.Count > 0 && _entities[0].Time < DateTime.UtcNow) {
                _entities.RemoveAt(0);
                _timer.Stop();
            }
            else
            {
                _timer.Stop();
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var services = CompositionRoot.Services;
            var rep = services.GetRequiredService<FeedingOrganizationService>();
            var newList = rep.GetSchedules();
            foreach (var ft in newList) {
                if (ft.Time < DateTime.UtcNow) {
                    Console.WriteLine($"Animal id={ft.Animal.Id} was fed at {ft.Time}!");
                    rep.RemoveSchedule(ft);
                }
            }
            rep.TimerUpdate();
        }
    }
}
