using HW_CPS_AntiPlag.Application;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_AntiPlag.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileAnalyzeController : HseAntiplagController
    {
        private readonly ILogger<FileAnalyzeController> _logger;

        public FileAnalyzeController(ILogger<FileAnalyzeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/statistics/{id}")]
        public async Task<IActionResult> GetStatResult(int id)
        {
            try
            {
                var response = await new FileAnalisysFacade().GetStatResult(id);
                return await Proxy(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/compare/{id1}/{id2}")]
        public async Task<IActionResult> GetCompareResult(int id1, int id2)
        {
            try { 
                var response = await new FileAnalisysFacade().GetCompareResult(id1, id2);
                return await Proxy(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
