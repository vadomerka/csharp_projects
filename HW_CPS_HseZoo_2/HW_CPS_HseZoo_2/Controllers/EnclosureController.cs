using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_HSEZoo_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnclosureController : ControllerBase
    {
        [HttpGet("/get/{enclosureId}", Name = "GetEnclosure")]
        public ActionResult<Enclosure> Get(int enclosureId)
        {
            try
            {
                var enclosure = EnclosureFacade.GetEnclosure(enclosureId);
                if (enclosure == null)
                {
                    return NotFound(new { message = $"Enclosure with ID {enclosureId} not found." });
                }
                return Ok(enclosure);
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

        [HttpPost("/create/{length}/{width}/{height}/{maxCount}", Name = "CreateEnclosure")]
        public ActionResult Post([FromBody] List<string> types, int length, int width, int height, [FromQuery] int maxCount)
        {
            try
            {
                EnclosureFacade.AddEnclosure(types, new EnclosureSize(length, width, height), maxCount);
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

        [HttpDelete("/delete/{enclosureId}", Name = "DeleteEnclosure")]
        public ActionResult Delete(int id)
        {
            try
            {
                EnclosureFacade.DeleteEnclosure(id);
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
