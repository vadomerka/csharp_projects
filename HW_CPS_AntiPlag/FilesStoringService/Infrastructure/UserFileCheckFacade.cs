using FilesStoringService.Models;
using FilesStoringService.Services;

namespace FilesStoringService.Infrastructure
{
    public class UserFileCheckFacade
    {
        private readonly FileDBContext _dbContext;

        public UserFileCheckFacade(FileDBContext context)
        {
            _dbContext = context;
        }

        public bool CheckEmpty(FileDTO dto) {
            var service = new UserFileCheckService(_dbContext);
            return service.CheckEmpty(dto);
        }

        public bool CheckFile(UserFile file) {
            var service = new UserFileCheckService(_dbContext);
            if (service.CheckExists(file)) { return false; }
            return true;
        }
    }
}
