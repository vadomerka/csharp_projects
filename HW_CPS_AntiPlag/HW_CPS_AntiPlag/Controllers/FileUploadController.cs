namespace HW_CPS_AntiPlag.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace HW_CPS_AntiPlag.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class FileUploadController : ControllerBase
        {
            private readonly ILogger<FileUploadController> _logger;

            public FileUploadController(ILogger<FileUploadController> logger)
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
