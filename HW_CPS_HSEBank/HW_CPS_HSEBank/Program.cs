using HW_CPS_HSEBank.DataParsing;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.UI;

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
            BankDataParser.Import<JsonDataParser>("tests/hseBank");

            BankUI.Menu();

            return 0;
        }
    }
}
