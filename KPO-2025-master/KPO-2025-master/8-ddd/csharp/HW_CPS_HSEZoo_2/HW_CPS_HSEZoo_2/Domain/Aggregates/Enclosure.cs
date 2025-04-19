using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Aggregates
{
    internal class Enclosure : IEnclosure
    {
        private List<IEnclosable> _animals = new List<IEnclosable>();
        private List<string> _enclosureTypes = new List<string>();
        private EnclosureSize _size;
        private int _count = 0;
        private int _maxCount = 0;

        public Enclosure() { }

        public Enclosure(EnclosureSize size, int maxCount) {
            _size = size;
            _maxCount = maxCount;
        }

        public int Count { get { return _count; } }
        public int MaxCount { get { return _maxCount; } set { _maxCount = value; } }

        public bool CheckAdd(IEnclosable animal) {
            return _count + 1 >= _maxCount;
        }

        public bool CheckRemove(IEnclosable animal)
        {
            return !_animals.Contains(animal);
        }

        public void AddEntity(IEnclosable animal) {
            if (CheckAdd(animal)) throw new OverflowException();
            _animals.Add(animal);
            _count = _animals.Count;
        }

        public void RemoveEntity(IEnclosable animal)
        {
            if (CheckRemove(animal)) { throw new ArgumentException(); }
            _animals.Remove(animal);
        }

        public void CleanUp() {
            Console.WriteLine("Cleaned Enclosure!");
        }
    }
}
