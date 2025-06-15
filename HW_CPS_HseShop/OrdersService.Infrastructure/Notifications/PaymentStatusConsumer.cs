using Confluent.Kafka;
using HseShopTransactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace OrdersService.Infrastructure.Notifications
{
    public class PaymentStatusConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PaymentStatusConsumer> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly string _topic;

        public PaymentStatusConsumer(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<PaymentStatusConsumer> logger,
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
            await Task.Yield();

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

                    _consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while consuming messages");
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
        }

        private async Task ProcessMessageAsync(OrderChange orderChange, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderDBContext>();

            var notification = new Notification(
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
