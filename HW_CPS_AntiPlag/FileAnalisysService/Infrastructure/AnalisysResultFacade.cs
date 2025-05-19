using FileAnalisysService.Infrastructure;
using FileAnalisysService.Models;
using FileAnalisysService.Services;

namespace FilesAnaliseService.Infrastructure
{
    public class AnalisysResultFacade
    {
        private readonly AnalisysDBContext _dbContext;

        public AnalisysResultFacade(AnalisysDBContext context) {
            _dbContext = context;
        }

        public async Task<FileStatistics> GetStatsResults(int fileId)
        {
            var fs = new FindAnalisysResultService(_dbContext);
            var res = fs.FindStatsByFileId(fileId);
            if (res != null) { return res; }

            var fas = new FileAnalyseService(_dbContext);
            var fcf = new FileContentsFacade();

            var fcontents = await fcf.GetFileContents(fileId);

            return fas.MakeFileStatistics(fcontents);
        }

        public async Task<FileCompare> GetCompareResults(int fileId1, int fileId2)
        {
            var fs = new FindAnalisysResultService(_dbContext);
            var res = fs.FindCompareByFileIds(fileId1, fileId2);
            if (res != null) { return res; }

            var fas = new FileAnalyseService(_dbContext);
            var fcf = new FileContentsFacade();

            var fcontents1 = await fcf.GetFileContents(fileId1);
            var fcontents2 = await fcf.GetFileContents(fileId2);

            return fas.MakeFileCompare(fcontents1, fcontents2);
        }
    }
}
