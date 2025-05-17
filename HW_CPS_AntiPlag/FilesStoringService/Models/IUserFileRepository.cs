namespace FilesStoringService.Models
{
    public interface IUserFileRepository
    {
        public IEnumerable<UserFile> GetEntities();
        public UserFile? GetEntity(int id);
        public int AddEntity(UserFile entity);
    }
}
