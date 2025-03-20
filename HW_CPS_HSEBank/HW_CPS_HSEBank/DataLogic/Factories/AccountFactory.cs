using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    /// <summary>
    /// Фабрика для создания аккаунтов
    /// </summary>
    public class AccountFactory : IDataFactory<BankAccount>
    {
        protected int lastId = 0;
        public int Id { set => lastId = value; }

        public BankAccount Create()
        {
            return new BankAccount(++lastId, "", 0);
        }

        public BankAccount Create(string name, decimal balance) { 
            BankAccount bankAccount = new BankAccount(++lastId, name, balance);
            return bankAccount;
        }

        public BankAccount Create(object[] args)
        {
            if (args.Length != 2) throw new ArgumentException();
            BankAccount bankAccount = new BankAccount(++lastId, (string)args[0], (decimal)args[1]);
            return bankAccount;
        }

        public BankAccount Create(BankAccount nAcc)
        {
            BankAccount bankAccount = new BankAccount(nAcc.Id, nAcc.Name, nAcc.Balance);
            return bankAccount;
        }
    }
}
