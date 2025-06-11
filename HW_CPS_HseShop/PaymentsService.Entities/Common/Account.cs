namespace PaymentsService.Entities.Common
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
