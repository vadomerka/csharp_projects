using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.UI;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank
{
    /// <summary>
    /// Класс программы.
    /// </summary>
    public class MainClass
    {
        public static int Main(string[] args)
        {
            // Раскомментировать строчку, для авто-импорта тестовых данных.
            var newrep = BankDataParser.Import<JsonDataParser>("tests/hseBank");
            var services = CompositionRoot.Services;
            var oldrep = services.GetRequiredService<BankDataManager>();
            oldrep.Save(newrep);

            BankUI.Menu();

            return 0;
        }
    }
}
