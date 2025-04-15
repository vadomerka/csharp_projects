using HW_CPS_HSEZoo_2.Domain.Entities;

namespace HW_CPS_HSEZoo_2.Domain.Aggregates
{
    interface IEnclosure
    {
        public bool CheckAdd(IEnclosable entity);
        public bool CheckRemove(IEnclosable entity);
        public void AddEntity(IEnclosable entity);
        public void RemoveEntity(IEnclosable entity);
    }
}
