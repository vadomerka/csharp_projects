using FilesStoringService.Models;

namespace FilesStoringService.Services
{
    public class FileCheckService
    {
        private IUserFileRepository _repository;
        private readonly FileDBContext _dbContext;

        public FileCheckService(FileDBContext context)
        {
            _dbContext = context;
            _repository = new UserFileRepository(_dbContext);
        }

        public bool CheckEmpty(FileDTO dto)
        {
            var file = dto.File;
            if (file == null) return true;
            return file.Length == 0;
        }

        public bool CheckId(UserFile file)
        {
            var res = _dbContext.UserFiles.Where(e =>
            e.Id == file.Id).ToList();
            if (res.Count == 0) return false;
            else return true;
        }

        public bool CheckHash(UserFile file) {
            var res = _dbContext.UserFiles.Where(e =>
            e.AuthorId == file.AuthorId && e.Hash == file.Hash).ToList();
            if (res.Count == 0) return false;
            else return true;
        }

        public bool CheckName(UserFile file)
        {
            var res = _dbContext.UserFiles.Where(e =>
            e.Name == file.Name).ToList();
            if (res.Count == 0) return false;
            else return true;
        }

        public bool CheckLocation(UserFile file)
        {
            var res = _dbContext.UserFiles.Where(e =>
            e.Location == file.Location).ToList();
            if (res.Count == 0) return false;
            else return true;
        }

        public bool CheckExists(UserFile file)
        {
            if (CheckId(file)) return true;
            if (CheckHash(file)) return true;
            if (CheckLocation(file)) return true;
            return false;
        }
    }
}
