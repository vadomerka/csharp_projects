using HseShopTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrdersService.Infrastructure.Facades;
using OrdersService.Infrastructure.Repositories;
using System.Text.Json;

namespace OrdersService.Infrastructure.Notifications
{
    public class PaymentStatusProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PaymentStatusProcessor> _logger;

        public PaymentStatusProcessor(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<PaymentStatusProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = await ProcessNotificationAsync(stoppingToken);

                    if (result == ProcessResult.AllProcessed)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing notifications");
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
        }

        private async Task<ProcessResult> ProcessNotificationAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderDBContext>();
            var faf = new FindOrderFacade(context);

            var notifications = await context.Notifications
                .Where(n => !n.IsProcessed)
                .OrderBy(n => n.CreatedAt)
                .Take(2)
                .ToListAsync(cancellationToken);

            if (!notifications.Any())
            {
                return ProcessResult.AllProcessed;
            }

            var notification = notifications.First();

            _logger.LogInformation("Processing notification {NotificationId}: {Payload}", notification.Id, notification.Payload);

            var orderChange = JsonSerializer.Deserialize<OrderChange>(notification.Payload) ?? throw new ArgumentException();
            var orp = new OrderRepository(context);
            var order = faf.FindOrderByOrderId(orderChange.OrderId);
            if (order != null) {
                order.Status = orderChange.Status;
                orp.Update(order);
            }


            await context.Notifications
                .Where(n => n.Id == notification.Id)
                .ExecuteUpdateAsync(
                    n => n.SetProperty(n => n.IsProcessed, true),
                    cancellationToken
                );

            return notifications.Count == 1
                ? ProcessResult.AllProcessed
                : ProcessResult.HasMore;
        }

        private enum ProcessResult
        {
            AllProcessed,
            HasMore
        }
    }
}
