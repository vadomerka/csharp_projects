using HW_CPS_HSEZoo_2.UseCases;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Infrastructure
{
    public class EnclosureFacade
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static IServiceProvider Services { get { return services; } set { services = value; } }
        public static IEnclosure GetEnclosure(int enId)
        {
            var rep = services.GetRequiredService<EnclosureDataService>();
            return rep.GetEnclosure(enId);
        }

        public static void AddEnclosure(List<string> types, EnclosureSize size, int maxCount)
        {
            var rep = services.GetRequiredService<EnclosureDataService>();
            rep.AddEnclosure(rep.Create(types, size, maxCount));
        }

        public static void DeleteEnclosure(int id)
        {
            var rep = services.GetRequiredService<EnclosureDataService>();
            rep.RemoveEnclosure(rep.GetEnclosure(id));
        }
    }
}
