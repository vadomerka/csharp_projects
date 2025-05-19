using FileAnalisysService.Models;
using FilesAnaliseService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilesAnaliseService.Controllers
{
    [ApiController]
    [Route("/api")]
    public class FileAnalyseController : ControllerBase
    {
        private readonly AnalisysDBContext _context;

        public FileAnalyseController(AnalisysDBContext context)
        {
            _context = context;
        }

        //[HttpGet("/statistics")]
        //public ActionResult<IEnumerable<AnalisysResult>> GetAllResults()
        //{
        //    var ur = new AnalisysResultFacade(_context);
        //    var files = ur.GetUserFiles().Select(e => new { e.Id } ).ToList();
        //    return Ok(files);
        //}

        [HttpGet("/statistics/{id}")]
        public async Task<ActionResult<FileStatistics>> GetStatResult(int id)
        {
            try
            {
                var arf = new AnalisysResultFacade(_context);
                var res = await arf.GetResults(id);
                return Ok(res);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest("Error while getting file contents.");
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("/compare/{id1}/{id2}")]
        public async Task<ActionResult<FileStatistics>> GetCompareResult(int id1, int id2)
        {
            try
            {
                var arf = new AnalisysResultFacade(_context);
                var res = await arf.Get(id);
                return Ok(res);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest("Error while getting file contents.");
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
