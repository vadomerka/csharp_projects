using FileAnaliseService.Infrastructure;
using FileAnaliseService.Models;
using FileAnaliseService.Services;

namespace FileAnaliseService.Application
{
    public class AnalisysResultFacade
    {
        private readonly AnalisysDBContext _dbContext;

        public AnalisysResultFacade(AnalisysDBContext context) {
            _dbContext = context;
        }

        public async Task<FileStatistics> GetStatsResults(int fileId)
        {
            // Проверка есть ли готовый результат.
            var fs = new FindAnalisysResultService(_dbContext);
            var res = fs.FindStatsByFileId(fileId);
            if (res != null) { return res; }

            // Получение содержимого.
            var fas = new FileAnalyseService(_dbContext);
            var fcf = new FileContentsService();
            var fcontents = await fcf.GetFileContents(new HttpClient(), fileId);

            // Анализ файла.
            return fas.MakeFileStatistics(fcontents);
        }

        public async Task<FileCompare> GetCompareResults(int fileId1, int fileId2)
        {
            // Проверка есть ли готовый результат.
            var fs = new FindAnalisysResultService(_dbContext);
            var res = fs.FindCompareByFileIds(fileId1, fileId2);
            if (res != null) { return res; }

            // Получение содержимого.
            var fas = new FileAnalyseService(_dbContext);
            var fcf = new FileContentsService();
            var fcontents1 = await fcf.GetFileContents(new HttpClient(), fileId1);
            var fcontents2 = await fcf.GetFileContents(new HttpClient(), fileId2);

            // Анализ файла.
            return fas.MakeFileCompare(fcontents1, fcontents2);
        }
    }
}
