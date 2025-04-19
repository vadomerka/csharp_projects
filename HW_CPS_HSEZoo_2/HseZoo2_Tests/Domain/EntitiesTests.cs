using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HseZoo2_Tests.Domain
{
    public class EntitiesTests
    {
        [Fact]
        public void Animal_EmptyConstructorTest()
        {
            var check = new Animal();

            Assert.Equal(new Animal(), check);
        }

        [Fact]
        public void Animal_ConstructorTest()
        {
            var check = new Animal();
            check.Species = "species";
            check.AnimalTypes = new List<string>() { "animalTypes1", "animalTypes2" };
            check.Name = "name";
            check.BirthDate = DateOnly.MaxValue;
            check.Gender = Gender.Female;
            check.FavouriteFood = "favouriteFood";
            check.IsHealthy = false;

            Assert.Equal(check, new Animal(0, "species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", false));
            Assert.Equal(check, new Animal(0, "species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", true));
            Assert.NotEqual(check, new Animal(2, "species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", false));
        }

        [Fact]
        public void FeedingSchedule_EmptyConstructorTest()
        {
            var check = new FeedingSchedule();

            Assert.Equal(new FeedingSchedule(), check);
        }

        [Fact]
        public void FeedingSchedule_ConstructorTest()
        {
            var check = new FeedingSchedule();
            check.Time = DateTime.MaxValue;
            check.FoodType = "foodType";
            var an = new Animal();
            check.Animal = an;

            Assert.Equal(check, new FeedingSchedule(0, an, DateTime.MaxValue, "foodType"));
            Assert.Equal(check, new FeedingSchedule(0, an, DateTime.MaxValue, "foodType2"));
            Assert.NotEqual(check, new FeedingSchedule(2, an, DateTime.MaxValue, "foodType1"));
        }
    }
}
