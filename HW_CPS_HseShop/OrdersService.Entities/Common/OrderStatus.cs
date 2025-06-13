namespace OrdersService.Entities.Common
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        //public Guid OrderId { get; set; }
        public string Payload { get; set; } = null!;
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
