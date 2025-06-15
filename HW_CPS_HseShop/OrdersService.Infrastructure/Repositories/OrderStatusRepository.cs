using HseShopTransactions;

namespace OrdersService.Infrastructure.Repositories
{
    public class OrderStatusRepository
    {
        private readonly OrderDBContext _dbContext;
        public OrderStatusRepository(OrderDBContext dbContext) { _dbContext = dbContext; }

        public IEnumerable<OrderStatus> GetAll() { 
            return _dbContext.OrderStatuses.ToList();
        }

        public OrderStatus? Get(Guid id) {
            return _dbContext.OrderStatuses.FirstOrDefault(x => x.Id == id);
        }

        public void Add(OrderStatus entity)
        {
            _dbContext.OrderStatuses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(OrderStatus entity)
        {
            _dbContext.OrderStatuses.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
