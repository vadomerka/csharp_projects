namespace FileAnaliseService.Models
{
    /// <summary>
    /// Результат сравнения двух файлов.
    /// </summary>
    public class FileCompare
    {
        public int Id { get; set; }
        public int FileId1 { get; set; }
        public int FileId2 { get; set; }
        public double Result { get; set; }
    }
}
