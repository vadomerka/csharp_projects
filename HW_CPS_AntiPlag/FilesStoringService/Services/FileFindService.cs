using FilesStoringService.Models;

namespace FilesStoringService.Services
{
    public class FileFindService
    {
        private readonly FileDBContext _dbContext;

        public FileFindService(FileDBContext context)
        {
            _dbContext = context;
        }

        public string FindFileContents(UserFile file) {
            var location = file.Location;
            if (location == null) throw new FileNotFoundException();
            if (!File.Exists(location)) throw new FileNotFoundException();

            var res = File.ReadAllText(location);
            return res;
        }
    }
}
