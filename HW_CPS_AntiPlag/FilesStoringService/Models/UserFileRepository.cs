namespace FilesStoringService.Models
{
    public class UserFileRepository : IUserFileRepository
    {
        private readonly FileDBContext _dbContext;

        public UserFileRepository(FileDBContext context) { _dbContext = context; }

        public IEnumerable<UserFile> GetEntities() { return _dbContext.UserFiles.ToList(); }

        public UserFile? GetEntity(int id) { return _dbContext.UserFiles.Find(id); }

        public int AddEntity(UserFile entity) { 
            var res = _dbContext.UserFiles.Add(entity); 
            _dbContext.SaveChanges();
            return res.Entity.Id;
        }
    }
}
