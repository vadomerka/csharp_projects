using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HW_CPS_HSEZoo_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        [HttpGet("/get/{enclosureId}/{animalId}", Name = "GetEnclosureAnimal")]
        public ActionResult<IEnclosable> Get(int enclosureId, int animalId)
        {
            try
            {
                var entity = AnimalFacade.GetAnimal(enclosureId, animalId);
                if (entity == null)
                {
                    return NotFound(new { message = $"Animal in enclosure {enclosureId} with ID {animalId} not found." });
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

        [HttpPost("/create/{enclosureId}", Name = "CreateAnimalInEnclosure")]
        public ActionResult Post(int enclosureId, AnimalDTO dto)
        {
            try
            {
                AnimalFacade.AddAnimal(enclosureId, dto);
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

        [HttpPut("/move/{fromId}/{animalId}/{toId}", Name = "MoveAnimal")]
        public ActionResult Move(int fromId, int animalId, int toId)
        {
            try
            {
                AnimalFacade.MoveAnimal(fromId, animalId, toId);
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

        [HttpDelete("/delete/{enclosureId}/{animalId}", Name = "DeleteAnimal")]
        public ActionResult Delete(int enclosureId, int animalId)
        {
            try
            {
                AnimalFacade.DeleteAnimal(enclosureId, animalId);
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
