using HseShopTransactions;
using HseShopTransactions.Infrastructure.Repositories;

namespace HseShopTransactions.Infrastructure.Facades
{
    public class OrderExecuter
    {
        private readonly AccountDBContext _dbContext;
        public OrderExecuter(AccountDBContext context) { _dbContext = context; }

        public OrderChange ExecuteOrder(OrderChange order) {
            var faf = new FindAccountFacade(_dbContext);
            var arp = new AccountRepository(_dbContext);
            OrderState os = new OrderState();

            var account = faf.FindAccountByUserId(order.UserId);
            if (account == null || account.Balance < order.Amount)
            {
                os = OrderState.Cancelled;
            }
            else
            {
                account.Balance -= order.Amount;
                arp.Update(account);

                os = OrderState.Finished;
            }

            return new OrderChange(
                order.OrderId,
                order.UserId,
                order.Amount,
                os,
                order.UpdatedAt
            );
        }
    }
}
