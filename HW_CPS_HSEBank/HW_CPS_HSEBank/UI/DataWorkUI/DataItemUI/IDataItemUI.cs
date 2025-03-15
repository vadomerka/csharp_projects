namespace HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI
{
    /// <summary>
    /// Интерфейс для использования темплейтных меню.
    /// </summary>
    public interface IDataItemUI
    {
        public string Title { get; }

        public bool AddItem();
        public bool FindItem();
        public bool DeleteItem();
        public bool ChangeItem();
    }
}
