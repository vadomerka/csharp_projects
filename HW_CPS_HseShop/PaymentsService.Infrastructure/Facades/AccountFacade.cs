using HseShopTransactions.Entities.Common;
using HseShopTransactions.Infrastructure.Repositories;
using HseShopTransactions.UseCases.Accounts;

namespace HseShopTransactions.Infrastructure.Facades
{
    public class AccountFacade
    {
        private readonly AccountDBContext _dbContext;
        public AccountFacade(AccountDBContext context) { _dbContext = context; }

        public IEnumerable<Account> GetAll()
        {
            var rep = new AccountRepository(_dbContext);
            var res = rep.GetAll();
            return res;
        }

        public Account GetAccount(Guid id)
        {
            var rep = new AccountRepository(_dbContext);
            var res = rep.Get(id);
            if (res == null) { throw new ArgumentException(); }
            return res;
        }

        public Account AddAccount(AccountDTO dto)
        {
            var rep = new AccountRepository(_dbContext);
            var faf = new FindAccountFacade(_dbContext);
            var fac = new AccountFactory();

            var check = faf.FindAccountByUserId(dto.UserId);
            if (check != null) { throw new ArgumentException("Этот пользователь уже владеет аккаунтом."); }

            var res = fac.Create(dto);
            rep.Add(res);
            return res;
        }

        public Account AddToAccount(Guid id, Decimal money)
        {
            var rep = new AccountRepository(_dbContext);
            var res = rep.Get(id);
            if (res == null) { throw new ArgumentException(); }
            res.Balance += money;
            rep.Update(res);
            return res;
        }
    }
}
