using OrdersService.Entities.Common;
using System.Text.Json;

namespace OrdersService.Infrastructure.Notifications
{
    public class SendNotificationService
    {
        private readonly OrderDBContext _context;

        public SendNotificationService(OrderDBContext context)
        {
            _context = context;
        }

        public async Task SendOrderNotificationAsync(Order order, CancellationToken cancellationToken)
        {
            var payload = new OrderChange(
                order.Id,
                order.UserId,
                order.Amount,
                order.Status,
                order.CreatedAt
            );

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
