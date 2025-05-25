using FilesStoringService.Models;

namespace FilesStoringService.Services
{
    /// <summary>
    /// Сервис для нахождения содержания файла.
    /// </summary>
    public class FileFindService
    {
        public FileFindService() { }

        public string FindFileContents(UserFile file) {
            var location = file.Location;
            if (location == null) throw new FileNotFoundException();
            if (!File.Exists(location)) throw new FileNotFoundException();

            var res = File.ReadAllText(location);
            return res;
        }
    }
}
