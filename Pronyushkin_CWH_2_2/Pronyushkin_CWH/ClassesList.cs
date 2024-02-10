using System.Text.RegularExpressions;

namespace ObjectClass
{
    /// <summary>
    /// Наследуемый класс. Содержит основные методы объекта.
    /// </summary>
    abstract class Dish
    {
        abstract public double CalculatePrice();
    }

    /// <summary>
    /// Класс куб. Наследник класса 3DShape
    /// </summary>
    class Appetizer : Dish
    {
        protected double _salad = 4;
        protected double _soup = 5;

        public override double CalculatePrice() 
        {
            return _salad + _soup;
        }

        public override string ToString() 
        {
            return $"Салат = {_salad: 0.00}; Суп = {_soup: 0.00};";
        }
    }

    class MainCourse : Dish
    {
        protected double _pasta = 5;
        protected double _beaten = 10;

        public override double CalculatePrice()
        {
            return 1.1 * (_pasta + _beaten);
        }

        public override string ToString()
        {
            return $"Макароны = {_pasta: 0.00}; Отбивная = {_beaten: 0.00};";
        }
    }

    class Dessert : Dish
    {
        protected double _cake = 15;
        protected double _iceCream = 7;

        public override double CalculatePrice()
        {
            return _cake + _iceCream;
        }
        public override string ToString()
        {
            return $"Торт = {_cake: 0.00}; Мороженое = {_iceCream: 0.00};";
        }
    }
}