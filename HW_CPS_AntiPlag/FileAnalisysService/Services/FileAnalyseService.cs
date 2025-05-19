using FileAnalisysService.Models;
using FilesAnaliseService;

namespace FileAnalisysService.Services
{
    public class FileAnalyseService
    {
        private readonly AnalisysDBContext _dbContext;

        public FileAnalyseService(AnalisysDBContext context)
        {
            _dbContext = context;
        }

        public FileStatistics MakeFileStatistics(FileContents file) {
            var res = new FileStatistics();
            var contents = file.Contents;
            if (contents == null) throw new ArgumentException();

            res.FileId = file.FileId;
            res.ParagraphsCount = contents.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
            res.WordsCount = contents.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            res.SymbolsCount = contents.Length;

            res = _dbContext.FileStatistics.Add(res).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public FileCompare MakeFileCompare(FileContents file1, FileContents file2)
        {
            var res = new FileCompare();
            res.Result = file1.Contents == file2.Contents ? 100 : 0;
            res.FileId1 = file1.FileId;
            res.FileId2 = file2.FileId;

            res = _dbContext.FileCompare.Add(res).Entity;
            _dbContext.SaveChanges();

            return res;
        }
    }
}
