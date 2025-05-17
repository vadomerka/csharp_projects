using FilesStoringService.Models;

namespace FilesStoringService.Services
{
    public class UserFileService
    {
        private IUserFileRepository _repository;
        public UserFileService(FileDBContext dBContext) { 
            _repository = new UserFileRepository(dBContext);
        }

        public IEnumerable<UserFile> GetUserFiles()
        {
            var res = _repository.GetEntities();
            if (res == null) throw new ArgumentException();
            return res;
        }

        public UserFile GetUserFile(int id) {
            var res = _repository.GetEntity(id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        public UserFile CreateUserFile(FileDTO dto, string location, string hash) {
            var factory = new UserFileFactory();
            var userFile = factory.Create(dto, location, hash);
            return userFile;
        }

        public int AddUserFile(UserFile entity)
        {
            return _repository.AddEntity(entity);
        }
    }
}
