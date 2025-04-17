using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Factories
{
    public class EnclosureFactory
    {
        private static int _id = 0;

        public static Enclosure Create(List<string> types, EnclosureSize size, int maxCount)
        {
            return new Enclosure(++_id, types, size, maxCount);
        }
    }
}
