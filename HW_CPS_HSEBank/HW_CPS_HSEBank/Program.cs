using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.UI;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var services = CompositionRoot.Services;
            var b = services.GetRequiredService<BankDataManager>();
            var f1 = services.GetRequiredService<AccountFactory>();
            var f2 = services.GetRequiredService<FinanceOperationFactory>();
            var f3 = services.GetRequiredService<CategoryFactory>();
            b.AddData(f1.Create("n1", 10));
            b.AddData(f1.Create("n2", 11));
            b.AddData(f1.Create("n3", 12));

            b.AddData(f3.Create("test category 1"));
            b.AddData(f3.Create("test category 2"));

            b.AddData(f2.Create("доход", 1, 50, DateTime.Now, "desc", 1));

            BankUI.Menu();
        }
    }
}
