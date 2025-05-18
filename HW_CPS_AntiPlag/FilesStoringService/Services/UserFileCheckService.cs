using FilesStoringService.Models;

namespace FilesStoringService.Services
{
    public class UserFileCheckService
    {
        private UserFileFindService _finder;

        public UserFileCheckService(FileDBContext context)
        {
            _finder = new UserFileFindService(context);
        }

        public bool CheckEmpty(FileDTO dto)
        {
            var file = dto.File;
            if (file == null) return true;
            return file.Length == 0;
        }

        // true if found.
        public bool CheckId(UserFile file) {
            return _finder.FindById(file.Id) != null;
        }

        // true if found.
        public bool CheckHash(UserFile file) {
            return _finder.FindByAuthorHash(file.AuthorId, file.Hash) != null;
        }

        // true if found.
        public bool CheckName(UserFile file) {
            return _finder.FindByName(file.Name) != null;
        }

        // true if found.
        public bool CheckLocation(UserFile file) {
            return _finder.FindByLocation(file.Location) != null;
        }

        // true if found.
        public bool CheckExists(UserFile file) {
            return CheckId(file) || CheckHash(file) || CheckLocation(file);
        }
    }
}
