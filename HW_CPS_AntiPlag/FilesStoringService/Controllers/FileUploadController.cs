using FilesStoringService.Infrastructure;
using FilesStoringService.Models;
using FilesStoringService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesStoringService.Controllers
{
    [ApiController]
    [Route("/api")]
    public class FileUploadController : ControllerBase
    {
        //private readonly ILogger<FileUploadController> _logger;
        private readonly FileDBContext _context;

        public FileUploadController(FileDBContext context)
        {
            _context = context;
        }

        [HttpGet("/files")]
        public ActionResult<IEnumerable<UserFile>> Get()
        {
            var ur = new UserFileFacade(_context);
            return Ok(ur.GetUserFiles());
        }

        [HttpGet("/file/{id}")]
        public ActionResult<UserFile> Get(int id)
        {
            var ur = new UserFileFacade(_context);
            return Ok(ur.GetUserFile(id));
        }

        [HttpPost("/file")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm] FileDTO dto)
        {
            var file = dto.File;
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var facade = new UserFileFacade(_context);
            try
            {
                //var uf = facade.AddDBUserFile(dto);
                //await facade.AddFile(dto, uf);
                var uf = await facade.SaveUserFileAsync(dto);

                return Ok(new { uf.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch {
                return BadRequest();
            }
        }
    }
}
