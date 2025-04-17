using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class AnimalDataService
    {
        private IServiceProvider services = CompositionRoot.Services;
        public Animal Create(AnimalDTO dto)
        {
            return AnimalFactory.Create(dto);
        }
        public Animal GetAnimal(IEnclosure enclosure, int id)
        {

            return (Animal)enclosure.GetEntity(id);
        }
    }
}
