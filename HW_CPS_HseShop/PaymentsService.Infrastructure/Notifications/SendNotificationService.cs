using HseShopTransactions;
using System.Text.Json;

namespace HseShopTransactions.Infrastructure.Notifications
{
    public class SendNotificationService
    {
        private readonly AccountDBContext _context;

        public SendNotificationService(AccountDBContext context)
        {
            _context = context;
        }

        public async Task SendOrderNotificationAsync(OrderChange orderChange, CancellationToken cancellationToken)
        {
            var payload = orderChange;

            var payloadJson = JsonSerializer.Serialize(payload);

            var notification = new OrderStatus(
                Guid.NewGuid(),
                payloadJson,
                isSent: false,
                DateTimeOffset.UtcNow
            );

            await _context.OrderStatuses.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
