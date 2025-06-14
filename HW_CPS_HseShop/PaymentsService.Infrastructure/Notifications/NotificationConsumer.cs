using Confluent.Kafka;
using HseShopTransactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentsService.Entities.Common;
using System.Text.Json;

namespace PaymentsService.Infrastructure.Notifications
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly string _topic;

        public NotificationConsumer(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<NotificationConsumer> logger,
            IConsumer<string, string> consumer,
            string topic)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _consumer = consumer;
            _topic = topic;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result is null)
                    {
                        continue;
                    }

                    var orderChange = JsonSerializer.Deserialize<OrderChange>(result.Message.Value);

                    if (orderChange is null)
                    {
                        _logger.LogWarning("Received invalid transaction change: {Message}", result.Message.Value);
                        continue;
                    }

                    await ProcessMessageAsync(orderChange, stoppingToken);

                    // Ручной коммит оффсета после успешной обработки сообщения
                    _consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while consuming messages");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private async Task ProcessMessageAsync(OrderChange orderChange, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AccountDBContext>();

            var notification = new Notification(
                Guid.NewGuid(),
                orderChange.Status,
                payload: JsonSerializer.Serialize(orderChange),
                isProcessed: false,
                DateTimeOffset.UtcNow
            );

            await context.Notifications.AddAsync(notification, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Received notification {NotificationId}: {Payload}", notification.Id, notification.Payload);
        }
    }
}
