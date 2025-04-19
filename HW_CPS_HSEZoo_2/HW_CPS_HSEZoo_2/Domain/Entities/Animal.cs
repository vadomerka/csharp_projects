using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    public class Animal : IAnimal
    {
        public Animal(int id = 0)
        {
            Species = "";
            AnimalTypes = new List<string>();
            Name = "";
            BirthDate = DateOnly.MinValue;
            Gender = Gender.Male;
            FavouriteFood = "";
            IsHealthy = true;
            Id = id;
        }

        public Animal(int id, string species, List<string> animalTypes, string name, 
            DateOnly birthDate, Gender gender, string favouriteFood, bool isHealthy) {
            Species = species;
            AnimalTypes = animalTypes;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavouriteFood = favouriteFood;
            IsHealthy = isHealthy;
            Id = id;
        }

        public string Species { get; set; }
        public List<string> AnimalTypes { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FavouriteFood { get; set; }
        public bool IsHealthy { get; set; }
        public int Id { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return ((Animal)obj).Id == Id;
        }
    }
}
