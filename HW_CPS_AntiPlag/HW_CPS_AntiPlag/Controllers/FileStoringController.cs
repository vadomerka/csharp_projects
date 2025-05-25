using HW_CPS_AntiPlag.Application;
using HW_CPS_AntiPlag.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_AntiPlag.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStoringController : HseAntiplagController
    {
        private readonly ILogger<FileStoringController> _logger;

        public FileStoringController(ILogger<FileStoringController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/files")]
        public async Task<IActionResult> Get()
        {
            try { 
                var response = await new FileStoringFacade().GetFiles();
                return await Proxy(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/file/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try { 
                var response = await new FileStoringFacade().GetFile(id);
                return await Proxy(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/file")]
        public async Task<IActionResult> Post([FromForm] FileDTO dto)
        {
            try { 
                var response = await new FileStoringFacade().PostFile(dto);
                return await Proxy(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
