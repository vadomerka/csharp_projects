using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Factories
{
    public class AnimalFactory
    {
        private int _id = 0;

        public Animal Create(AnimalDTO dto)
        {
            return new Animal(++_id, dto.Species, dto.AnimalTypes, dto.Name, 
                dto.BirthDate, dto.Gender, dto.FavouriteFood, dto.IsHealthy);
        }
    }
}
