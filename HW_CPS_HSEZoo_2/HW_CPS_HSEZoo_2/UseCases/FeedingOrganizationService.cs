using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Events;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using System.Timers;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class FeedingOrganizationService
    {
        public static event ZooEvents.FeedingTimeEvent? feedEvent;

        private static System.Timers.Timer _timer = new System.Timers.Timer();
        private IServiceProvider services = CompositionRoot.Services;

        public FeedingOrganizationService() {
            _timer.Elapsed += (sender, e) => OnTimedEvent(sender, e);
        }
        public FeedingOrganizationService(IServiceProvider sservices) {
            _timer.Elapsed += (sender, e) => OnTimedEvent(sender, e);
            services = sservices; 
        }

        public List<ISchedule> GetSchedules()
        {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            return rep.GetEntities();
        }

        public ISchedule GetSchedule(int id)
        {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            return rep.GetEntity(id);
        }

        public ISchedule Create(IAnimal animal, FeedingScheduleDTO dto)
        {
            return services.GetRequiredService<FeedingScheduleFactory>().Create(animal, dto);
        }

        public void AddSchedule(ISchedule enclosure)
        {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            rep.AddEntity(enclosure);
            TimerUpdate();
        }

        public void RemoveSchedule(ISchedule enclosure)
        {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            if (!rep.CheckRemove(enclosure)) throw new ArgumentException();
            rep.RemoveEntity(enclosure);
            TimerUpdate();
        }

        public void RemoveSchedules(IAnimal animal) {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            rep.RemoveEntitiesByAnimal(animal);
            TimerUpdate();
        }

        private void TimerUpdate()
        {
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            rep.SortByTime();
            var _entities = rep.GetEntities();

            if (_entities.Count > 0 && _entities[0].Time >= DateTime.UtcNow)
            {
                try
                {
                    double when = (_entities[0].Time - DateTime.UtcNow).TotalMilliseconds;
                    _timer.Close();
                    _timer = new System.Timers.Timer(when);
                    _timer.AutoReset = false;
                    _timer.Elapsed += (sender, e) => OnTimedEvent(sender, e);
                    _timer.Start();
                } catch (Exception) {}
            } else if (_entities.Count > 0 && _entities[0].Time < DateTime.UtcNow) {
                _entities.RemoveAt(0);
                _timer.Stop();
            }
            else
            {
                _timer.Stop();
            }
        }

        private static void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            var services = CompositionRoot.Services;
            var rep = services.GetRequiredService<FeedingOrganizationService>();
            try
            {
                if (rep.GetSchedules().Count > 0) { 
                
                    ISchedule sch = rep.GetSchedules()[0];
                    var ft = rep.GetSchedule(sch.Id);

                    Console.WriteLine($"\t{ft.Id}: Animal id={ft.Animal.Id} was fed at {ft.Time}!");
                    feedEvent?.Invoke(sch);

                    rep.RemoveSchedule(ft);
                }
            } catch
            {
                Console.WriteLine("Feeding error!");
            }
            rep.TimerUpdate();
        }
    }
}
