namespace OrdersService.Entities.Common
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public OrderState Status { get; set; }
    }
}
