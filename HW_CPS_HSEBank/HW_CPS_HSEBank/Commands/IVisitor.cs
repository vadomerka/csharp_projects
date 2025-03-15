namespace HW_CPS_HSEBank.Commands
{
    /// <summary>
    /// Нереализованный интерфейс.
    /// </summary>
    public interface IVisitor
    {
        public void Execute(ICommand command);
    }
}
