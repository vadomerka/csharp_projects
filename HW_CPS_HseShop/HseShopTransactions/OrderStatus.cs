namespace HseShopTransactions
{
    public class OrderStatus
    {
        public Guid Id { get; }
        public string Payload { get; }
        public bool IsSent { get; }
        public DateTimeOffset CreatedAt { get; }

        public OrderStatus(Guid id, string payload, bool isSent, DateTimeOffset createdAt)
        {
            Id = id;
            Payload = payload;
            IsSent = isSent;
            CreatedAt = createdAt;
        }
    }
}
