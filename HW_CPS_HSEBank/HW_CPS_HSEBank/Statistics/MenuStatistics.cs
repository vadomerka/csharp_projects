using HW_CPS_HSEBank.DataParsing;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.UI.MenuUtils;

namespace HW_CPS_HSEBank.Statistics
{
    public class MenuStatistics
    {
        private List<IMenuCommand> _stats;

        public MenuStatistics() { 
            _stats = new List<IMenuCommand>();
        }

        public void AddStats(IMenuCommand command) { 
            _stats.Add(command);
        }

        public void WriteToFile(string fileName) {
            ListParser<JsonDataParser>.Export(_stats, fileName + JsonDataParser.GetExtension());
        }
    }
}
