namespace OrdersService.Entities.Common
{
    public class OrderDTO
    {
        public Guid UserId { get; set; }
        public Decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public OrderState Status { get; set; }
    }
}
