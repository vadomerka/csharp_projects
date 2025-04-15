using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    internal class Animal : IEnclosable, IFeedable
    {
        private List<string> _animalTypes = new List<string>();

        public Animal() {
            Species = "";
            Name = "";
            BirthDate = DateOnly.MinValue;
            Gender = Gender.Male;
            FavouriteFood = "";
            IsHealthy = true;
        }

        public Animal(string species, List<string> animalTypes, string name, 
            DateOnly birthDate, Gender gender, string favouriteFood, bool isHealthy) {
            Species = species;
            _animalTypes = animalTypes;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavouriteFood = favouriteFood;
            IsHealthy = isHealthy;
        }

        public string Species { get; set; }
        public List<string> AnimalTypes { get { return _animalTypes; } }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FavouriteFood { get; set; }
        public bool IsHealthy { get; set; }

        public void AddType (string type)
        {
            type = type.ToLower();
            if (_animalTypes.Contains(type)) return;
            _animalTypes.Add(type);
        }

        public void Feed(string foodTipe) {
            Console.WriteLine($"Animal is fed with {foodTipe}!");
        }
    }
}
