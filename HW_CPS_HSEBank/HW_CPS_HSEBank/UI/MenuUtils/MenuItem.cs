namespace HW_CPS_HSEBank.UI.MenuUtils
{
    // Метод меню.
    public delegate bool UIFunc();

    /// <summary>
    /// Класс для пункта меню.
    /// </summary>
    public class MenuItem : IMenuCommand
    {
        public string _title;
        public UIFunc _func;
        public DateTime _start;

        public MenuItem(string title, UIFunc func)
        {
            _title = title;
            _func = func;
            _start = DateTime.Now;
        }

        public string Type => "Menu";
        public string Title => _title;

        public DateTime StartTime => _start;

        public double Duration => 0;

        public bool Execute()
        {
            bool res = _func();
            return res;
        }
    }
}
