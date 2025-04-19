using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Repositories
{
    public class EnclosureRepository : IEnclosureRepository
    {
        private List<IEnclosure> enclosures = new List<IEnclosure>();

        public EnclosureRepository() { }

        public List<IEnclosure> GetEntities() { return enclosures; }

        public IEnclosure GetEntity(int id)
        {
            var res = enclosures.Find((x) => x.Id == id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        public void AddEntity(IEnclosure enclosure)
        {
            enclosures.Add(enclosure);
        }

        public void RemoveEntity(IEnclosure enclosure)
        {
            enclosures.Remove(enclosure);
        }
    }
}
