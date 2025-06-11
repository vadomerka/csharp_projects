using OrdersService.Entities.Common;
using OrdersService.Infrastructure.Repositories;
using OrdersService.UseCases.Orders;

namespace OrdersService.Infrastructure.Facades
{
    public class OrderFacade
    {
        private readonly OrderDBContext _dbContext;
        public OrderFacade(OrderDBContext context) { _dbContext = context; }

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

        public Order AddOrder(OrderDTO dto)
        {
            var rep = new OrderRepository(_dbContext);
            //var faf = new FindOrderFacade(_dbContext);
            var fac = new OrderFactory();

            //var check = faf.FindOrderByUserId(dto.UserId);
            //if (check != null) { throw new ArgumentException("Этот пользователь уже владеет аккаунтом."); }

            var res = fac.Create(dto);
            rep.Add(res);
            return res;
        }

        //public Order AddToOrder(Guid id, Decimal money)
        //{
        //    var rep = new OrderRepository(_dbContext);
        //    var res = rep.Get(id);
        //    if (res == null) { throw new ArgumentException(); }
        //    res.Balance += money;
        //    rep.Update(res);
        //    return res;
        //}
    }
}
