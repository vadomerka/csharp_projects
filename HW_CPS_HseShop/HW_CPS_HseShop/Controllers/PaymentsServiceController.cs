using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_HseShop.Controllers
{
    [ApiController]
    [Route("/api")]
    public class PaymentsServiceController : ControllerBase
    {
        private readonly ILogger<PaymentsServiceController> _logger;

        public PaymentsServiceController(ILogger<PaymentsServiceController> logger)
        {
            _logger = logger;
        }

        
    }
}
