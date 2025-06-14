using Microsoft.AspNetCore.Mvc;
using OrdersService.Entities.Common;
using OrdersService.Infrastructure;
using OrdersService.Infrastructure.Facades;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("/api")]
    public class OrdersServiceController : ControllerBase
    {
        private readonly OrderDBContext _context;
        //private readonly CancellationToken _cancellationToken;

        public OrdersServiceController(OrderDBContext context)
        {
            _context = context;
            //_cancellationToken = cancellationToken;
        }

        [HttpGet("/orders")]
        public ActionResult<IEnumerable<Order>> Get(CancellationToken _cancellationToken)
        {
            var ur = new OrderFacade(_context, _cancellationToken);
            var accs = ur.GetAll();
            return Ok(accs);
        }

        [HttpGet("/order/{id}")]
        public ActionResult<Order> Get(CancellationToken _cancellationToken, Guid id)
        {
            var ur = new OrderFacade(_context, _cancellationToken);
            try
            {
                var res = ur.GetOrder(id);
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

        [HttpPost("/order")]
        public IActionResult Post(CancellationToken _cancellationToken, [FromForm] OrderDTO dto)
        {
            var facade = new OrderFacade(_context, _cancellationToken);
            try
            {
                var res = facade.AddOrder(dto);

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
