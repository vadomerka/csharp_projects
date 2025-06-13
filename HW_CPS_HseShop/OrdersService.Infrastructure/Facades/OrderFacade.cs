using OrdersService.Entities.Common;
using OrdersService.Infrastructure.Notifications;
using OrdersService.Infrastructure.Repositories;
using OrdersService.UseCases.Orders;

namespace OrdersService.Infrastructure.Facades
{
    public class OrderFacade
    {
        private readonly OrderDBContext _dbContext;
        private readonly CancellationToken _cancellationToken;

        public OrderFacade(OrderDBContext context, CancellationToken cancellationToken) { 
            _dbContext = context; 
            _cancellationToken = cancellationToken;
        }

        public IEnumerable<Order> GetAll()
        {
            var rep = new OrderRepository(_dbContext);
            var res = rep.GetAll();
            return res;
        }

        public Order GetOrder(Guid id)
        {
            var rep = new OrderRepository(_dbContext);
            var res = rep.Get(id);
            if (res == null) { throw new ArgumentException(); }
            return res;
        }

        public async Task<Order> AddOrder(OrderDTO dto)
        {
            var orp = new OrderRepository(_dbContext);
            var osr = new OrderStatusRepository(_dbContext);
            var son = new SendNotificationService(_dbContext);
            var fac = new OrderFactory();

            var order = fac.Create(dto);
            orp.Add(order);

            await son.SendOrderNotificationAsync(order, _cancellationToken);

            return order;
        }
    }
}
