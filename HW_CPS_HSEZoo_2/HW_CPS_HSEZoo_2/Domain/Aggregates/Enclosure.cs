using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Aggregates
{
    public class Enclosure : IEnclosure
    {
        private List<IEnclosable> _animals = new List<IEnclosable>();
        private List<string> _enclosureTypes = new List<string>();
        private EnclosureSize _size;
        private int _count = 0;
        private int _maxCount = 0;

        public Enclosure(int id = 0) {
            Id = id;
        }

        public Enclosure(int id, List<string> types, EnclosureSize size, int maxCount) {
            Id = id;
            _enclosureTypes = types;
            _size = size;
            _maxCount = maxCount;
        }

        public int Id { get; }
        public int Count { get { return _count; } }
        public int MaxCount { get { return _maxCount; } set { _maxCount = value; } }
        public List<IEnclosable> Animals { get { return _animals; } }


        public bool CheckAdd(IEnclosable animal) {
            return !HasEntity(animal.Id) && _count >= _maxCount;
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

        public IEnclosable GetEntity(int id) {
            var res = _animals.Find((x) => x.Id == id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        private bool HasEntity(int id)
        {
            var res = _animals.Find((x) => x.Id == id);
            return res != null;
        }

        public void CleanUp() {
            Console.WriteLine("Cleaned Enclosure!");
        }
    }
}
