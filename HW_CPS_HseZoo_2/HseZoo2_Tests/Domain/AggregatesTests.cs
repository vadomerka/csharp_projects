using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HseZoo2_Tests.Domain
{
    public class AggregatesTests
    {
        [Fact]
        public void Enclosure_EmptyConstructorTest()
        {
            var check = new Enclosure();

            Assert.Equal(new Enclosure(), check);
        }

        [Fact]
        public void Enclosure_ConstructorTest()
        {
            var check = new Enclosure(1);
            check.EnclosureTypes = new List<string>() { "types" };
            check.Size = new EnclosureSize(1, 1, 1);
            check.MaxCount = 2;

            Assert.Equal(check, new Enclosure(1, new List<string>() { "types" }, new EnclosureSize(1, 1, 1), 2));

            Assert.NotNull(check.GetEntities());
            Assert.False(check.GetEntities().Any());
            var ent = new Animal();
            check.AddEntity(ent);
            Assert.True(check.GetEntities().Any());
            Assert.Equal(ent, check.GetEntity(ent.Id));
            check.RemoveEntity(ent);
            Assert.False(check.GetEntities().Any());
            Assert.Throws<ArgumentException>(() => check.GetEntity(ent.Id));
        }
    }
}
