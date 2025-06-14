namespace OrdersService.Entities.Common
{
    public record OrderChange(
        Guid OrderId,
        Guid UserId,
        decimal Amount,
        OrderState Status,
        DateTime UpdatedAt
    );
}
