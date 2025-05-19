using FileAnalisysService.Models;
using FilesAnaliseService;

namespace FileAnalisysService.Services
{
    public class FindAnalisysResultService
    {
        private readonly AnalisysDBContext _dbContext;

        public FindAnalisysResultService(AnalisysDBContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<FileStatistics> FindStatisticsResults() { 
            return _dbContext.FileStatistics;
        }

        public IEnumerable<FileCompare> FindCompareResults()
        {
            return _dbContext.FileCompare;
        }

        public FileStatistics? FindStatsById(int id)
        {
            var res = _dbContext.FileStatistics.Where(e => e.Id == id);
            if (res.Any()) { return res.First(); }
            return null;
        }

        public FileStatistics? FindStatsByFileId(int fileId)
        {
            var res = _dbContext.FileStatistics.Where(e => e.FileId == fileId);
            if (res.Any()) { return res.First(); }
            return null;
        }

        public FileCompare? FindCompareById(int id)
        {
            var res = _dbContext.FileCompare.Where(e => e.Id == id);
            if (res.Any()) { return res.First(); }
            return null;
        }

        public FileCompare? FindCompareByFileIds(int fid1, int fid2)
        {
            var res = _dbContext.FileCompare.Where(e => e.FileId1 == fid1 && e.FileId2 == fid2);
            if (res.Any()) { return res.First(); }
            return null;
        }


        public IEnumerable<FileCompare> FindCompareByFileId(int fid1)
        {
            var res = _dbContext.FileCompare.Where(e => e.FileId1 == fid1 || e.FileId2 == fid1);
            return res;
        }

        public IEnumerable<FileCompare> FindCompareByFileId1(int fid1)
        {
            var res = _dbContext.FileCompare.Where(e => e.FileId1 == fid1);
            return res;
        }

        public IEnumerable<FileCompare> FindCompareByFileId2(int fid1)
        {
            var res = _dbContext.FileCompare.Where(e => e.FileId2 == fid1);
            return res;
        }
    }
}
