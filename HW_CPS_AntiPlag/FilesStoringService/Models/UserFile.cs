namespace FilesStoringService.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int AuthorId { get; set; }
        public string Hash { get; set; } = null!;
        public string Location { get; set; } = null!;
    }
}
