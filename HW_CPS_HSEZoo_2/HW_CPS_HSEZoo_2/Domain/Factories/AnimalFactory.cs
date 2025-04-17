using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Factories
{
    public static class AnimalFactory
    {
        private static int _id = 0;

        public static Animal Create(string species, List<string> animalTypes, string name,
            DateOnly birthDate, Gender gender, string favouriteFood, bool isHealthy)
        {
            return new Animal(++_id, species, new List<string> { "" }, "", DateOnly.MinValue, ValueObjects.Gender.Male, "", true);
        }

        public static Animal Create(AnimalDTO dto)
        {
            return new Animal(++_id, dto.Species, dto.AnimalTypes, dto.Name, dto.BirthDate, dto.Gender, dto.FavouriteFood, dto.IsHealthy);
        }
    }
}
