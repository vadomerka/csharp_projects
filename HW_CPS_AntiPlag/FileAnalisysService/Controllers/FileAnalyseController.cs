using FileAnaliseService.Application;
using FileAnaliseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileAnaliseService.Controllers
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

        [HttpGet("/statistics/{id}")]
        public async Task<ActionResult<FileStatistics>> GetStatResult(int id)
        {
            try
            {
                var arf = new AnalisysResultFacade(_context);
                var res = await arf.GetStatsResults(id);
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
                var res = await arf.GetCompareResults(id1, id2);
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
