namespace FilesStoringService.Models
{
    public interface IUserFileRepository
    {
        public IEnumerable<UserFile> GetEntities();
        public UserFile? GetEntity(int id);
        public UserFile? GetEntity(Func<UserFile, bool> lam);
        public int AddEntity(UserFile entity);
    }
}
