using FilesStoringService.DataBase;
using FilesStoringService.Models;
using FilesStoringService.Services;

namespace FilesStoringService.Infrastructure
{
    public class UserFileFacade
    {
        private readonly FileDBContext _dbContext;

        public UserFileFacade(FileDBContext context) {
            _dbContext = context;
        }

        public IEnumerable<UserFile> GetUserFiles() {
            var fs = new UserFileService(_dbContext);
            return fs.GetUserFiles();
        }

        public UserFile GetUserFile(int id)
        {
            var fs = new UserFileService(_dbContext);
            return fs.GetUserFile(id);
        }

        public async Task<UserFile> SaveUserFileAsync(FileDTO dto) {
            var fs = new UserFileService(_dbContext);
            var fths = new FileToHashService();
            var ufcf = new UserFileCheckFacade(_dbContext);
            var fss = new FileSavingService();
            if (ufcf.CheckEmpty(dto)) throw new ArgumentNullException();

            var ufile = CreateFileFromDTO(dto);
            if (!ufcf.CheckFile(ufile))
            {
                // Если существующий файл найден.
                var flist = fs.GetUserFiles().Where(
                    (e) => e.AuthorId == ufile.AuthorId && e.Hash == ufile.Hash).ToList();
                ufile = flist[0];
            }
            else
            {
                // Если нужно создавать новый.
                int id = fs.AddUserFile(ufile);
                ufile.Id = id;
                await fss.AddFile(dto.File, ufile.Location);
            }

            return ufile;
        }

        public UserFile CreateFileFromDTO(FileDTO dto) {
            var fs = new UserFileService(_dbContext);
            var fths = new FileToHashService();

            var hash = fths.GetHash(dto.File);
            string fileName = $"{dto.AuthorId}_{hash}.txt";
            var location = Path.Combine("DataBase", "UserFiles", fileName);
            var ufile = fs.CreateUserFile(dto, location, hash);
            return ufile;
        }

        // Добавляет файл в базу данных
        public UserFile AddDBUserFile(FileDTO dto)
        {
            var fs = new UserFileService(_dbContext);
            var fths = new FileToHashService();
            var ufcf = new UserFileCheckFacade(_dbContext);

            if (ufcf.CheckEmpty(dto)) throw new ArgumentNullException();

            var ufile = CreateFileFromDTO(dto);

            if (!ufcf.CheckFile(ufile)) throw new ArgumentException();

            int id = fs.AddUserFile(ufile);
            ufile.Id = id;

            return ufile;
        }
    }
}
