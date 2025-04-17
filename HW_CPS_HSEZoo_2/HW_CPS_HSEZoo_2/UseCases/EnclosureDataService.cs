using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class EnclosureDataService
    {
        private IServiceProvider services = CompositionRoot.Services;
        public IEnclosure GetEnclosure(int id) { 
            var rep = services.GetRequiredService<EnclosureRepository>();
            return rep.GetEntity(id);
        }

        public IEnclosure Create(List<string> types, EnclosureSize size, int maxCount) {
            return EnclosureFactory.Create(types, size, maxCount);
        }

        public void AddEnclosure(IEnclosure enclosure)
        {
            var rep = services.GetRequiredService<EnclosureRepository>();
            rep.AddEntity(enclosure);
        }

        public void RemoveEnclosure(IEnclosure enclosure) {
            var rep = services.GetRequiredService<EnclosureRepository>();
            rep.RemoveEntity(enclosure);
        }
    }
}
