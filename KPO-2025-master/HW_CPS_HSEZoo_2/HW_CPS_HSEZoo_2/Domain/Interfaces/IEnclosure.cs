﻿using HW_CPS_HSEZoo_2.Domain.Entities;

namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface IEnclosure : IEntity
    {
        public List<IEnclosable> GetEntities();
        public IEnclosable GetEntity(int Id);
        public bool CheckAdd(IEnclosable entity);
        public bool CheckRemove(IEnclosable entity);
        public void AddEntity(IEnclosable entity);
        public void RemoveEntity(IEnclosable entity);
    }
}
