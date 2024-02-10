// Пронюшкин Радомир БПИ234-1 СР
using ObjectClass;

namespace Pronyushkin_SR
{
    public class MainProgram
    {
        /// <summary>
        /// Основная часть программы. Реализован цикл повтора решения.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                List<Dish> dishList = new List<Dish>();
                do
                {
                    Console.WriteLine("Нажмите номер желаемого блюда на клавиатуре:");
                    Console.WriteLine("1) Салат и суп");
                    Console.WriteLine("2) Макароны и отбивная");
                    Console.WriteLine("3) Торт и мороженое");

                    switch (Console.ReadKey().Key)
                    {
                        case (ConsoleKey.D1):
                            dishList.Add(new Appetizer());
                            break;
                        case (ConsoleKey.D2):
                            dishList.Add(new MainCourse());
                            break;
                        case (ConsoleKey.D3):
                            dishList.Add(new Dessert());
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Вы не выбрали ни одного блюда.");
                            break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Чтобы выбрать новое блюдо, нажмите 'Enter'.");

                } while (Console.ReadKey().Key == ConsoleKey.Enter);

                Dish[] dishArray = dishList.ToArray();
                double summa = 0;
                Console.WriteLine();
                for (int i = 0; i < dishArray.Length; i++)
                {
                    summa += dishArray[i].CalculatePrice();
                    Console.WriteLine(dishArray[i]);
                }
                Console.WriteLine($"Сумма заказа: {summa: 0.00}");

                // Реализация цикла повтора решения.
                Console.WriteLine("Чтобы продолжить, нажмите 'Enter'.");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}