namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    public struct AnimalDTO
    {
        public AnimalDTO() {
            Species = "";
            Name = "";
            AnimalTypes = new List<string>();
            BirthDate = DateOnly.MinValue;
            Gender = Gender.Male;
            FavouriteFood = "";
            IsHealthy = true;
        }

        public AnimalDTO(string species, List<string> animalTypes, string name,
            DateOnly birthDate, Gender gender, string favouriteFood, bool isHealthy) {
            Species = species;
            AnimalTypes = animalTypes;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavouriteFood = favouriteFood;
            IsHealthy = isHealthy;
        }

        public string Species { get; set; }
        public List<string> AnimalTypes { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string FavouriteFood { get; set; }
        public bool IsHealthy { get; set; }
        public int Id { get; }

        public override bool Equals(object? obj) { 
            if (obj == null) return false;
            if (((AnimalDTO)obj).Species != Species) return false;
            var firstNotSecond = ((AnimalDTO)obj).AnimalTypes.Except(AnimalTypes).ToList();
            var secondNotFirst = AnimalTypes.Except(((AnimalDTO)obj).AnimalTypes).ToList();
            if (!(!firstNotSecond.Any() && !secondNotFirst.Any())) return false;
            if (((AnimalDTO)obj).Name != Name ) return false;
            if (((AnimalDTO)obj).BirthDate != BirthDate ) return false;
            if (((AnimalDTO)obj).Gender != Gender ) return false;
            if (((AnimalDTO)obj).FavouriteFood != FavouriteFood ) return false;
            if (((AnimalDTO)obj).IsHealthy != IsHealthy ) return false;
            if (((AnimalDTO)obj).Id != Id ) return false;
            return true;
        }
    }
}
