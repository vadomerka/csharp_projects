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
            var payload = new OrderDTO(
                transaction.Id,
                transaction.SubjectId,
                transaction.PeerId,
                transaction.Amount,
                ToContractStatus(status),
                transaction.CreatedAt
            );

            var payloadJson = JsonSerializer.Serialize(payload);

            var notification = new TransactionChangeNotification(
                Guid.NewGuid(),
                payloadJson,
                isSent: false,
                DateTimeOffset.UtcNow
            );

            await _context.TransactionChangeNotifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private static ContractsStatus ToContractStatus(ModelsStatus status)
        {
            return status switch
            {
                ModelsStatus.Hold => ContractsStatus.Hold,
                ModelsStatus.Charge => ContractsStatus.Charge,
                ModelsStatus.Cancel => ContractsStatus.Cancel,
                _ => throw new ArgumentException($"Invalid transaction status: {status}", nameof(status))
            };
        }
    }

}
