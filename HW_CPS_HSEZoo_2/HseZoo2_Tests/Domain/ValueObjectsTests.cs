using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HseZoo2_Tests.Domain
{
    public class ValueObjectsTests
    {
        [Fact]
        public void AnimalDTO_EmptyConstructorTest()
        {
            var check = new AnimalDTO();

            Assert.Equal(new AnimalDTO(), check);
        }

        [Fact]
        public void AnimalDTO_ConstructorTest()
        {
            var check = new AnimalDTO();
            check.Species = "species";
            check.AnimalTypes = new List<string>() { "animalTypes1", "animalTypes2" };
            check.Name = "name";
            check.BirthDate = DateOnly.MaxValue;
            check.Gender = Gender.Female;
            check.FavouriteFood = "favouriteFood";
            check.IsHealthy = false;

            Assert.Equal(check, new AnimalDTO("species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", false));
            Assert.NotEqual(check, new AnimalDTO("species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", true));
        }

        [Fact]
        public void FeedingScheduleDTO_EmptyConstructorTest()
        {
            var check = new FeedingScheduleDTO();

            Assert.Equal(new FeedingScheduleDTO(), check);
        }

        [Fact]
        public void FeedingScheduleDTO_ConstructorTest()
        {
            var check = new FeedingScheduleDTO();
            check.Time = DateTime.MaxValue;
            check.FoodType = "foodType";

            Assert.Equal(check, new FeedingScheduleDTO(DateTime.MaxValue, "foodType"));
            Assert.NotEqual(check, new FeedingScheduleDTO(DateTime.MaxValue, "foodType2"));
        }

        [Fact]
        public void EnclosureSize_EmptyConstructorTest()
        {
            var check = new EnclosureSize();

            Assert.Equal(new EnclosureSize(), check);
        }

        [Fact]
        public void EnclosureSize_ConstructorTest()
        {
            var check = new EnclosureSize();
            check.Length = 123;
            check.Width = 321;
            check.Height = 213;

            Assert.Equal(check, new EnclosureSize(123, 321, 213));
            Assert.NotEqual(check, new EnclosureSize(123, 321, 2133));
        }
    }
}
