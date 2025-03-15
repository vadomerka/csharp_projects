namespace HW_CPS_HSEBank.UI
{
    // Метод меню.
    public delegate bool UIFunc();

    /// <summary>
    /// Класс для пункта меню.
    /// </summary>
    public struct MenuItem
    {
        public string _title;
        public UIFunc _func;

        public MenuItem(string title, UIFunc func)
        {
            _title = title;
            _func = func;
        }
    }
}
