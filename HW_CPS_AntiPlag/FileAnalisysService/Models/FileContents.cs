namespace FileAnaliseService.Models
{
    /// <summary>
    /// Id и Содержимое файла.
    /// </summary>
    public class FileContents
    {
        public int FileId { get; set; }
        public string Contents { get; set; } = null!;
    }
}
