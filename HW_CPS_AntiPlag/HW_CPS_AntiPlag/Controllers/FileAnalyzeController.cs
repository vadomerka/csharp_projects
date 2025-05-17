namespace HW_CPS_AntiPlag.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace HW_CPS_AntiPlag.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class FileAnalyzeController : ControllerBase
        {
            private readonly ILogger<FileAnalyzeController> _logger;

            public FileAnalyzeController(ILogger<FileAnalyzeController> logger)
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
