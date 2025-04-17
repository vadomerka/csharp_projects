namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    public class AnimalDTO
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
    }
}
