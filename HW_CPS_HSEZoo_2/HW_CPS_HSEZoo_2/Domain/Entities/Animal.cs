using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using System.Xml.Linq;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    internal class Animal
    {
        private string _species = "";
        private List<string> _animalTypes = new List<string>();
        private string _name = "";
        private DateOnly _birthDate = new DateOnly();
        private Gender _gender = Gender.Male;
        private string _favouriteFood = "";
        private bool _isHealthy = true;
        private bool _isFed = false;

        public Animal() { }

        public Animal(string species, List<string> animalTypes, string name, 
            DateOnly birthDate, Gender gender, string favouriteFood, bool isHealthy) {
            _species = species;
            _animalTypes = animalTypes;
            _name = name;
            _birthDate = birthDate;
            _gender = gender;
            _favouriteFood = favouriteFood;
            _isHealthy = isHealthy;
        }

        public string Species { get; set; }
        public List<string> AnimalTypes { get; set; }
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

        public void Feed(string foodType) {
            if (foodType == _favouriteFood) {
                _isFed = true;
            }
        }

        // ??
        public void Move() { }
    }
}
