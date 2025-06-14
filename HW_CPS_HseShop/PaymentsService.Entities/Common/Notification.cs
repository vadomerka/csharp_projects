using HseShopTransactions;

namespace PaymentsService.Entities.Common
{
    public class Notification
    {
        public Guid Id { get; }
        public OrderState NotificationKey { get; }
        public string Payload { get; }
        public bool IsProcessed { get; }
        public DateTimeOffset CreatedAt { get; }

        public Notification(Guid id, OrderState notificationKey, string payload, bool isProcessed, DateTimeOffset createdAt)
        {
            Id = id;
            NotificationKey = notificationKey;
            Payload = payload;
            IsProcessed = isProcessed;
            CreatedAt = createdAt;
        }
    }
}
