using HW_CPS_HSEZoo.Interfaces;
using HW_CPS_HSEZoo.Models;
using HW_CPS_HSEZoo.Models.Inventory.Animals;
using HW_CPS_HSEZoo.Models.Inventory.Things;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HSEZoo_Tests
{
    public class AppConfigTests
    {
        private readonly Mock<HseZoo> _mockHseZoo;
        private readonly Mock<VetClinic> _mockVetClinic;
        private readonly Mock<InventoryFactory> _mockInventoryFactory;

        public AppConfigTests()
        {
            _mockHseZoo = new Mock<HseZoo>();
            _mockVetClinic = new Mock<VetClinic>();
            _mockInventoryFactory = new Mock<InventoryFactory>();
        }

        [Fact]
        public void AppConfigServices_ShouldReturnSameInstance()
        {
            var services1 = AppConfig.Services;
            var services2 = AppConfig.Services;

            Assert.Same(services1, services2);
        }

        [Fact]
        public void AppConfigServices_ShouldProvideRegistratedServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_mockHseZoo.Object);
            services.AddSingleton(_mockVetClinic.Object);
            services.AddSingleton(_mockInventoryFactory.Object);
            var serviceProvider = services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<HseZoo>());
            Assert.NotNull(serviceProvider.GetService<VetClinic>());
            Assert.NotNull(serviceProvider.GetService<InventoryFactory>());
        }
    }

    public class HseZooTests
    {
        [Fact]
        public void HseZoo_ShouldMakeEmptyList()
        {
            var hseZoo = new HseZoo();
            var list1 = hseZoo.GetInventoryData<IInventory>();
            int len1 = list1 == null ? 0 : list1.Count;

            Assert.Equal(0, len1);
        }

        [Fact]
        public void HseZoo_ShouldAddItemToList()
        {
            var hseZoo = new HseZoo();
            var animal = new Monkey(0);
            hseZoo.AddToInventory(animal);
            var list2 = hseZoo.GetInventoryData<IInventory>();
            var item1 = list2[0];

            Assert.Same(animal, item1);
        }

        [Fact]
        public void HseZoo_ShouldReturnNeededList()
        {
            var hseZoo = new HseZoo();
            var inventoryList = new List<IInventory>() { new Table(0), new Computer(1), new Monkey(2), new Tiger(3) };
            var checkList = new List<IAlive>();
            for (int i = 0; i < inventoryList.Count; i++)
            {
                hseZoo.AddToInventory(inventoryList[i]);
                if (inventoryList[i] is IAlive) { checkList.Add((IAlive)inventoryList[i]); }
            }

            Assert.Equal(checkList, hseZoo.GetInventoryData<IAlive>());
        }

        [Fact]
        public void HseZoo_ShouldReturnEmptyList()
        {
            var hseZoo = new HseZoo();
            var inventoryList = new List<IInventory>() { new Table(0), new Computer(1), new Monkey(2), new Tiger(3) };
            var checkList = new List<Rabbit>();
            for (int i = 0; i < inventoryList.Count; i++)
            {
                hseZoo.AddToInventory(inventoryList[i]);
                if (inventoryList[i] is Rabbit) { checkList.Add((Rabbit)inventoryList[i]); }
            }

            Assert.Equal(checkList, hseZoo.GetInventoryData<Rabbit>());
        }
    }

    public class InventoryFactoryTests
    {
        public static IEnumerable<object[]> GetTestAnimType()
        {
            yield return new object[] { "Monkey", new Monkey(0) };
            yield return new object[] { "Rabbit", new Rabbit(0) };
            yield return new object[] { "Tiger", new Tiger(0) };
            yield return new object[] { "Wolf", new Wolf(0) };
            yield return new object[] { "WoLf", new Wolf(0) };
            yield return new object[] { "WOLF", new Wolf(0) };
        }

        [Theory]
        [MemberData(nameof(GetTestAnimType))]
        public void InventoryFactory_ShouldCreateAnimal(string animType, Animal res)
        {
            var factory = new InventoryFactory();
            var anim = factory.CreateAnimal(animType);

            Assert.NotNull(anim);
            Assert.Equal(res.Number, anim.Number);
            Assert.Equal(res.Food, anim.Food);
        }

        [Fact]
        public void InventoryFactory_ShouldThrowAnimException()
        {
            var factory = new InventoryFactory();

            Assert.Throws<ArgumentException>(() => factory.CreateAnimal("not implemented type"));
        }

        public static IEnumerable<object[]> GetTestThingType()
        {
            yield return new object[] { "Table", new Table(0) };
            yield return new object[] { "Computer", new Computer(0) };
            yield return new object[] { "TaBle", new Table(0) };
            yield return new object[] { "TABLE", new Table(0) };
        }

        [Theory]
        [MemberData(nameof(GetTestThingType))]
        public void InventoryFactory_ShouldCreateThing(string thingType, Thing res)
        {
            var factory = new InventoryFactory();
            var anim = factory.CreateThing(thingType);

            Assert.NotNull(anim);
            Assert.Equal(res.Number, anim.Number);
        }

        [Fact]
        public void InventoryFactory_ShouldThrowThingException()
        {
            var factory = new InventoryFactory();

            Assert.Throws<ArgumentException>(() => factory.CreateThing("not implemented type"));
        }
    }

    public class VetClinicTests
    {
        [Fact]
        public void VetClinic_ShouldReturnFalse()
        {
            var factory = new VetClinic(1f);

            Assert.False(factory.AnalyzeHealth(new Monkey(0)));
        }

        [Fact]
        public void VetClinic_ShouldReturnTrue()
        {
            var factory = new VetClinic(0f);

            Assert.True(factory.AnalyzeHealth(new Monkey(0)));
        }
    }

    public class InventoryTests
    {
        [Fact]
        public void Thing_ShouldCreateTable()
        {
            var inv = new Table(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
        }

        [Fact]
        public void Thing_ShouldCreateComputer()
        {
            var inv = new Computer(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
        }

        [Fact]
        public void Predator_ShouldCreateTiger()
        {
            var inv = new Tiger(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
            Assert.Equal(0, inv.Food);
            inv.Food = 1;
            Assert.Equal(1, inv.Food);
        }

        [Fact]
        public void Predator_ShouldCreateWolf()
        {
            var inv = new Wolf(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
            Assert.Equal(0, inv.Food);
            inv.Food = 1;
            Assert.Equal(1, inv.Food);
        }

        [Fact]
        public void Herbo_ShouldCreateMonkey()
        {
            var inv = new Monkey(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
            Assert.Equal(0, inv.Food);
            inv.Food = 1;
            Assert.Equal(1, inv.Food);
            Assert.Equal(0, inv.Kindness);
            inv.Kindness = 1;
            Assert.Equal(1, inv.Kindness);
        }

        [Fact]
        public void Herbo_ShouldCreateRabbit()
        {
            var inv = new Rabbit(0);

            Assert.NotNull(inv);
            Assert.Equal(0, inv.Number);
            Assert.Equal(0, inv.Food);
            inv.Food = 1;
            Assert.Equal(1, inv.Food);
            Assert.Equal(0, inv.Kindness);
            inv.Kindness = 1;
            Assert.Equal(1, inv.Kindness);
        }
    }
}