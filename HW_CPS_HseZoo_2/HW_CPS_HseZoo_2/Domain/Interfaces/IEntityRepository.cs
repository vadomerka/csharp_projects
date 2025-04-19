namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface IEntityRepository<Entity> where Entity : class, IEntity
    {
        public List<Entity> GetEntities();

        public Entity GetEntity(int id);

        public void AddEntity(Entity enclosure);

        public void RemoveEntity(Entity enclosure);
    }
}
