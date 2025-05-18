using FilesStoringService.Services;

namespace FilesStoringService.Infrastructure
{
    public class FileContentsFacade
    {
        private readonly FileDBContext _dbContext;

        public FileContentsFacade(FileDBContext context)
        {
            _dbContext = context;
        }

        public string GetContents(int id) {
            var uffs = new UserFileFindService(_dbContext);
            var ffs = new FileFindService(_dbContext);
            var file = uffs.FindById(id);
            if (file == null) throw new ArgumentException();

            var res = ffs.FindFileContents(file);
            return res;
        }
    }
}
