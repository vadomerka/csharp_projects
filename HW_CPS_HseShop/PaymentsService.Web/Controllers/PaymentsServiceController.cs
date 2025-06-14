using Microsoft.AspNetCore.Mvc;
using PaymentsService.Entities.Common;
using PaymentsService.Infrastructure;
using PaymentsService.Infrastructure.Facades;

namespace FilesStoringService.Controllers
{
    [ApiController]
    [Route("/api")]
    public class PaymentsServiceController : ControllerBase
    {
        private readonly AccountDBContext _context;

        public PaymentsServiceController(AccountDBContext context)
        {
            _context = context;
        }

        [HttpGet("/accounts")]
        public ActionResult<IEnumerable<Account>> Get()
        {
            var ur = new AccountFacade(_context);
            var accs = ur.GetAll();
            return Ok(accs);
        }

        [HttpGet("/account/{id}")]
        public ActionResult<Account> Get(Guid id)
        {
            var ur = new AccountFacade(_context);
            try
            {
                var res = ur.GetAccount(id);
                return Ok(res);
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("/account/{id}")]
        public IActionResult AddMoney(Guid id, Decimal money)
        {
            var facade = new AccountFacade(_context);
            try
            {
                var res = facade.AddToAccount(id, money);

                return Ok(new { res });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("/account")]
        public IActionResult Post([FromForm] AccountDTO dto)
        {
            var facade = new AccountFacade(_context);
            try
            {
                var res = facade.AddAccount(dto);

                return Ok(new { res.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch {
                return BadRequest();
            }
        }
    }
}
