using OrdersService.Entities.Common;
using OrdersService.Infrastructure.Repositories;

namespace OrdersService.Infrastructure.Facades
{
    public class FindOrderFacade
    {
        private readonly OrderDBContext _dbContext;
        public FindOrderFacade(OrderDBContext context) { _dbContext = context; }

        public Order? FindOrderByUserId(Guid userId)
        {
            var rep = new OrderRepository(_dbContext);
            var res =  rep.GetAll().FirstOrDefault(x => x.UserId == userId);
            return res;
        }

        public Order? FindOrderByOrderId(Guid OrderId)
        {
            var rep = new OrderRepository(_dbContext);
            var res = rep.GetAll().FirstOrDefault(x => x.Id == OrderId);
            return res;
        }
    }
}
