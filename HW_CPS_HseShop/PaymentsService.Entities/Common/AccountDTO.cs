namespace PaymentsService.Entities.Common
{
    public class AccountDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
    }
}
