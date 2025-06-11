using PaymentsService.Entities.Common;

namespace PaymentsService.UseCases.Accounts
{
    public class AccountFactory
    {
        public AccountFactory() { }

        public Account Create(AccountDTO dto) {
            var res = new Account();
            res.UserId = dto.UserId;
            res.Name = dto.Name;
            res.Balance = 0;
            return res;
        }
    }
}
