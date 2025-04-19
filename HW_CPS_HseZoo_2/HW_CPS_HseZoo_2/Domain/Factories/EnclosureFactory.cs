using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Factories
{
    public class EnclosureFactory
    {
        private int _id = 0;

        public Enclosure Create(List<string> types, EnclosureSize size, int maxCount)
        {
            return new Enclosure(++_id, types, size, maxCount);
        }
    }
}
