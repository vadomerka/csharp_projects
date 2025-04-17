using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    public class Animal : IEnclosable, IFeedable
    {
        private List<string> _animalTypes = new List<string>();

        public Animal(int id = 0)
        {
            Species = "";
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
            _animalTypes = animalTypes;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavouriteFood = favouriteFood;
            IsHealthy = isHealthy;
            Id = id;
        }

        public string Species { get; set; }
        public List<string> AnimalTypes { get { return _animalTypes; } }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FavouriteFood { get; set; }
        public bool IsHealthy { get; set; }
        public int Id { get; }

        //public void AddType (string type)
        //{
        //    type = type.ToLower();
        //    if (_animalTypes.Contains(type)) return;
        //    _animalTypes.Add(type);
        //}

        //public void Feed(string foodTipe) {
        //    Console.WriteLine($"Animal is fed with {foodTipe}!");
        //}
    }
}
