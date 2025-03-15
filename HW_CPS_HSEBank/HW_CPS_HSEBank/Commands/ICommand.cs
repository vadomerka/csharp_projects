namespace HW_CPS_HSEBank.Commands
{
    /// <summary>
    /// Интерфейс команд.
    /// </summary>
    public interface ICommand
    {
        public string Type{ get; }
        public void Execute();
    }
}
