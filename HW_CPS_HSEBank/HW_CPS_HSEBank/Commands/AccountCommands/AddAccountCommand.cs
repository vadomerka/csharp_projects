using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.Data.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.Commands.AccountCommands
{
    public class AddAccountCommand : IBankOperation
    {
        private readonly string name = "";
        private readonly decimal balance = 0;

        public AddAccountCommand(string name, decimal balance)
        {
            this.name = name;
            this.balance = balance;
        }

        public string Type => "Add";

        public void Execute()
        {
            IServiceProvider services = CompositionRoot.Services;
            var accountFactory = services.GetRequiredService<AccountFactory>();
            var mb = services.GetRequiredService<BankDataRepository>();
            mb.AddAccount(accountFactory.Create(name, balance));
        }

        public void VisitorExecute(IVisitor visitor)
        {
            visitor.Execute(this);
        }
    }
}
