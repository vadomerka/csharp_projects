using Microsoft.EntityFrameworkCore;
using PaymentsService.Entities.Common;

namespace PaymentsService.Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly AccountDBContext _dbContext;
        public AccountRepository(AccountDBContext dbContext) { _dbContext = dbContext; }

        public IEnumerable<Account> GetAll() { 
            return _dbContext.Accounts.ToList();
        }

        public Account? Get(Guid id) {
            return _dbContext.Accounts.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Account entity)
        {
            _dbContext.Accounts.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Account entity)
        {
            _dbContext.Accounts.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
