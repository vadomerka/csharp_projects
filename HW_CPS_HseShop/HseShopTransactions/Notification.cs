namespace HseShopTransactions
{
    public class Notification
    {
        public Guid Id { get; }
        public string Payload { get; }
        public bool IsProcessed { get; }
        public DateTimeOffset CreatedAt { get; }

        public Notification(string payload, bool isProcessed, DateTimeOffset createdAt)
        {
            Payload = payload;
            IsProcessed = isProcessed;
            CreatedAt = createdAt;
        }
    }
}
