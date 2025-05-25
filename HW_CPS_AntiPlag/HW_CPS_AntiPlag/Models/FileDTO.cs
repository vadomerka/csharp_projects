using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_AntiPlag.Models
{
    /// <summary>
    /// Дто для FileStoringController
    /// </summary>
    public class FileDTO
    {
        public int AuthorId { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; } = null!;
    }
}
