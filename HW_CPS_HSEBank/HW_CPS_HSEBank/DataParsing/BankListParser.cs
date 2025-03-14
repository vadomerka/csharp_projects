using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.DataParsing.DataParserUtils;

namespace HW_CPS_HSEBank.DataParsing
{
    public static class BankListParser<Parser> where Parser : IDataToText
    {
        public static IEnumerable<TData> Import<TData>(string? fileName = null) where TData : class
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return DataToConsole<Parser>.ImportData<TData>();
            }
            return DataToFile<Parser>.ImportData<TData>(fileName);
        }

        public static void Export<TData>(IEnumerable<TData> records, string? fileName = null) where TData : class
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                DataToConsole<Parser>.ExportData<TData>(records);
                return;
            }
            DataToFile<Parser>.ExportData<TData>(records, fileName);
        }
    }
}
