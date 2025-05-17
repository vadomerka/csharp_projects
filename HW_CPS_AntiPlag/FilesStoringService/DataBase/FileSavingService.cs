using FilesStoringService.Models;

namespace FilesStoringService.DataBase
{
    public class FileSavingService
    {
        public FileSavingService() { }

        // Добавляет файл в файловую систему
        public async Task AddFile(IFormFile file, string location)
        {
            using (var stream = File.Create(location))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
