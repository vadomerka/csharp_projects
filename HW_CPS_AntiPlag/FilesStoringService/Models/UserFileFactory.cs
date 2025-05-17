namespace FilesStoringService.Models
{
    public class UserFileFactory
    {
        private int id = 0;
        public UserFileFactory() { }

        public UserFile Create(FileDTO dto, string location, string hash) {
            var res = new UserFile();
            //res.Id = ++id;
            res.Name = dto.File.FileName;
            res.AuthorId = dto.AuthorId;
            res.Location = location;
            res.Hash = hash;
            return res;
        }
    }
}
