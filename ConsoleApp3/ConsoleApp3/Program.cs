// Пронюшкин Радомир БПИ234-1 КДЗ-3-1 Вариант 17
using Functions;

namespace MainProgram
{
    /// <summary>
    /// Класс основной программы. Работает с пользователем.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Метод Main - основной метод программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Главный цикл программы.
            bool program = true;
            Function g = new Function(
                    (double y) => y * y + y
                    );
            Function f = new Function(
                (double x) => Math.Sin(x)
                );
            CompFunction compFunction = new CompFunction(g, f);
            while (program)
            {
                double a, b, delta;
                bool res = false;
                do
                {
                    Console.WriteLine("Введите вещественное значение а.");
                    res = !double.TryParse(Console.ReadLine(), out a);
                } while (res);
                do
                {
                    Console.WriteLine("Введите вещественное значение b (> a).");
                    res = !double.TryParse(Console.ReadLine(), out b) || b <= a;
                } while (res);
                do
                {
                    Console.WriteLine("Введите вещественное значение 0 < delta < 1.");
                    res = !double.TryParse(Console.ReadLine(), out delta) || !(0 < delta && delta < 1);
                } while (res);

                double result;
                for (double i = a; i < b; i += delta)
                {
                    result = compFunction.GetFuncResult(i);
                    Console.WriteLine(" {0:f3} | {1:f3}", i, result);
                }

                if (program)
                {
                    // Задержка между действиями.
                    Console.WriteLine("Введите 'Enter', чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
    }
}