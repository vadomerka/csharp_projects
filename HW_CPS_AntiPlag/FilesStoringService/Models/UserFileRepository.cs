namespace FilesStoringService.Models
{
    public class UserFileRepository : IUserFileRepository
    {
        private readonly FileDBContext _dbContext;

        public UserFileRepository(FileDBContext context) { _dbContext = context; }

        public IEnumerable<UserFile> GetEntities() { return _dbContext.UserFiles.ToList(); }

        public UserFile? GetEntity(int id) { return _dbContext.UserFiles.Find(id); }

        public UserFile? GetEntity(Func<UserFile, bool> lam) {
            var res = _dbContext.UserFiles.Where(lam).ToList();
            if (res.Count == 0) return null;
            else return res[0];
        }

        public int AddEntity(UserFile entity) { 
            var res = _dbContext.UserFiles.Add(entity); 
            _dbContext.SaveChanges();
            return res.Entity.Id;
        }
    }
}
