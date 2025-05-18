using FilesStoringService.Models;
using System.Collections.Generic;

namespace FilesStoringService.Services
{
    public class UserFileFindService
    {
        private IUserFileRepository _repository;

        public UserFileFindService(FileDBContext context)
        {
            _repository = new UserFileRepository(context);
        }

        public UserFile? FindById(int id) { return _repository.GetEntity(e => e.Id == id); }
        public UserFile? FindByHash(string hash) { return _repository.GetEntity(e => e.Hash == hash); }
        public UserFile? FindByAuthorHash(int authorId, string hash) { 
            return _repository.GetEntity(e => e.AuthorId == authorId && e.Hash == hash); 
        }
        public UserFile? FindByName(string name) { return _repository.GetEntity(e => e.Name == name); }
        public UserFile? FindByLocation(string location) { return _repository.GetEntity(e => e.Location == location); }

        public UserFile? FindByAny(UserFile file)
        {
            var res = FindById(file.Id); if (res != null) return res;
            res = FindByHash(file.Hash); if (res != null) return res;
            res = FindByName(file.Name); if (res != null) return res;
            res = FindByLocation(file.Location); if (res != null) return res;
            return null;
        }
    }
}
