using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class AnimalDataService
    {
        private IServiceProvider services = CompositionRoot.Services;

        public AnimalDataService() { }
        public AnimalDataService(IServiceProvider sservices) { services = sservices; }

        public IAnimal Create(AnimalDTO dto)
        {
            return services.GetRequiredService<AnimalFactory>().Create(dto);
        }
        public IAnimal GetAnimal(IEnclosure enclosure, int id)
        {
            return (IAnimal)enclosure.GetEntity(id);
        }
    }
}
