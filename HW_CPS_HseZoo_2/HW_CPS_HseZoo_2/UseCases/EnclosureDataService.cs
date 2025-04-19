using Humanizer;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class EnclosureDataService
    {
        private IServiceProvider services = CompositionRoot.Services;

        public EnclosureDataService() { }
        public EnclosureDataService(IServiceProvider sservices) { services = sservices; }

        public IEnclosure GetEnclosure(int id) { 
            var rep = services.GetRequiredService<EnclosureRepository>();
            return rep.GetEntity(id);
        }

        public IEnclosure Create(List<string> types, EnclosureSize size, int maxCount) {
            return services.GetRequiredService<EnclosureFactory>().Create(types, size, maxCount);
        }

        public void AddEnclosure(IEnclosure enclosure)
        {
            var rep = services.GetRequiredService<EnclosureRepository>();
            rep.AddEntity(enclosure);
        }

        public void RemoveEnclosure(IEnclosure enclosure) {
            var rep = services.GetRequiredService<EnclosureRepository>();
            var ans = services.GetRequiredService<AnimalTransferService>();
            var animals = enclosure.GetEntities();
            while (animals.Count > 0) {
                ans.Remove(enclosure, animals[0]);
            }
            rep.RemoveEntity(enclosure);
        }
    }
}
