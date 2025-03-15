namespace HW_CPS_HSEBank.Commands
{
    /// <summary>
    /// Интерфейс команд, работающими с банком.
    /// </summary>
    public interface IBankOperation : ICommand
    {
        public string? ToString();
    }
}
