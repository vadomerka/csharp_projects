using OrdersService.Entities.Common;

namespace OrdersService.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly OrderDBContext _dbContext;
        public OrderRepository(OrderDBContext dbContext) { _dbContext = dbContext; }

        public IEnumerable<Order> GetAll() { 
            return _dbContext.Orders.ToList();
        }

        public Order? Get(Guid id) {
            return _dbContext.Orders.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
