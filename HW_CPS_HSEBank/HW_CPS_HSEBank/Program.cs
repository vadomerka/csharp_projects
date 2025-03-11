using HW_CPS_HSEBank.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var b = CompositionRoot.Services.GetRequiredService<BankDataRepository>();
            b.AddAccount(new BankAccount(1, "n1", 10));
            b.AddAccount(new BankAccount(2, "n2", 11));
            b.AddAccount(new BankAccount(3, "n3", 12));

            b.AddCategory(new Category(1, "test", "test category 1"));
            b.AddCategory(new Category(2, "test2", "test category 2"));

            b.AddFinanceOperation(new FinanceOperation(1, "adsasd", 1, 654654, DateTime.Now, "desc", 1));

            BankUI.Menu();
        }
    }
}
