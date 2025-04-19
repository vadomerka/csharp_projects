using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Aggregates
{
    public class Enclosure : IEnclosure
    {
        private List<IEnclosable> _animals = new List<IEnclosable>();
        private int _count = 0;

        public Enclosure(int id = 0) {
            Id = id;
            EnclosureTypes = new List<string>();
        }

        public Enclosure(int id, List<string> types, EnclosureSize size, int maxCount) {
            Id = id;
            EnclosureTypes = types;
            Size = size;
            MaxCount = maxCount;
        }

        public int Id { get; }
        public int Count { get { return _count; } }
        public int MaxCount { get; set; }
        public EnclosureSize Size { get; set; }
        public List<string> EnclosureTypes { get; set; }
        public List<IAnimal> Animals { 
            get 
            {
                return _animals.Cast<IAnimal>().ToList();
            } 
        }

        public bool CheckAdd(IEnclosable animal) {
            return !HasEntity(animal.Id) && _count >= MaxCount;
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
            _animals.Remove((Animal)animal);
        }

        public IEnclosable GetEntity(int id) {
            var res = _animals.Find((x) => x.Id == id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        public List<IEnclosable> GetEntities()
        {
            return _animals;
        }

        private bool HasEntity(int id)
        {
            var res = _animals.Find((x) => x.Id == id);
            return res != null;
        }

        public void CleanUp() {
            Console.WriteLine("Cleaned Enclosure!");
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return ((Enclosure)obj).Id == Id;
        }
    }
}
