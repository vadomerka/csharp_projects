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

        public OrdersServiceController(OrderDBContext context)
        {
            _context = context;
        }

        [HttpGet("/orders")]
        public ActionResult<IEnumerable<Order>> Get()
        {
            var ur = new OrderFacade(_context);
            var accs = ur.GetAll();
            return Ok(accs);
        }

        [HttpGet("/order/{id}")]
        public ActionResult<Order> Get(Guid id)
        {
            var ur = new OrderFacade(_context);
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

        //[HttpPut("/account/{id}")]
        //public IActionResult AddMoney(Guid id, Decimal money)
        //{
        //    var facade = new OrderFacade(_context);
        //    try
        //    {
        //        var res = facade.AddToOrder(id, money);

        //        return Ok(new { res });
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("/order")]
        public async Task<IActionResult> Post([FromForm] OrderDTO dto)
        {
            var facade = new OrderFacade(_context);
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
