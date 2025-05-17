namespace HW_CPS_AntiPlag.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace HW_CPS_AntiPlag.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class FileDownloadController : ControllerBase
        {
            private readonly ILogger<FileDownloadController> _logger;

            public FileDownloadController(ILogger<FileDownloadController> logger)
            {
                _logger = logger;
            }
            
            [HttpGet(Name = "GetWeatherForecast")]
            public IEnumerable<int> Get()
            {
                return new List<int> { 1, 2 };
            }
        }
    }
}
