using Humanizer;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_HSEZoo_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedingTimeController : Controller
    {
        [HttpGet(Name = "GetFeedingTimes")]
        public ActionResult<ISchedule> Get(int id)
        {
            try
            {
                var entity = FeedingScheduleFacade.GetFeedingSchedule(id);
                if (entity == null)
                {
                    return NotFound(new { message = $"FeedingSchedule with ID {id} not found." });
                }
                return Ok(entity);
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

        [HttpPost(Name = "CreateFeedingTime")]
        public ActionResult Post(int enclosureId, int animalId, FeedingScheduleDTO dto)
        {
            try
            {
                FeedingScheduleFacade.AddFeedingSchedule(enclosureId, animalId, dto);
                return Ok();
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

        [HttpDelete(Name = "DeleteFeedingTime")]
        public ActionResult Delete(int id)
        {
            try
            {
                FeedingScheduleFacade.DeleteFeedingSchedule(id);
                return Ok();
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
