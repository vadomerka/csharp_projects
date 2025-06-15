using HseShopTransactions.Entities.Common;
using HseShopTransactions.Infrastructure.Repositories;
using HseShopTransactions.UseCases.Accounts;

namespace HseShopTransactions.Infrastructure.Facades
{
    public class FindAccountFacade
    {
        private readonly AccountDBContext _dbContext;
        public FindAccountFacade(AccountDBContext context) { _dbContext = context; }

        public Account? FindAccountByUserId(Guid userId)
        {
            var rep = new AccountRepository(_dbContext);
            var res =  rep.GetAll().FirstOrDefault(x => x.UserId == userId);
            //if (res == null) { throw new ArgumentException(); }
            return res;
        }
    }
}
