using HW_CPS_HSEZoo_2.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_HSEZoo_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        [HttpGet("/feedingStats", Name = "GetFeedingStats")]
        public ActionResult<List<string>> GetFeeding()
        {
            try
            {
                return Ok(ZooStatisticsFacade.GetFeedingStats());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }

        }

        [HttpGet("/movingStats", Name = "GetMovingStats")]
        public ActionResult<List<string>> GetMoving()
        {
            try
            {
                return Ok(ZooStatisticsFacade.GetMovingStats());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }

        }
    }
}
