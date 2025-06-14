using OrdersService.Entities.Common;

namespace OrdersService.UseCases.Orders
{
    public class OrderFactory
    {
        public OrderFactory() { }

        public Order Create(OrderDTO dto) {
            var res = new Order();
            res.UserId = dto.UserId;
            res.Amount = dto.Amount;
            res.Description = dto.Description;
            res.Status = dto.Status;
            res.CreatedAt = DateTime.UtcNow;
            return res;
        }
    }
}
