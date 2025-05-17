using Microsoft.AspNetCore.Mvc;

namespace FilesStoringService.Models
{
    public class FileDTO
    {
        public int AuthorId { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = null!;
    }
}
