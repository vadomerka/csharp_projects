using PointLibrary;

namespace Control_3_1_31
{
    public class Program
    {
        static void Main()
        {
            Console.Write("Введите координату X: ");
            if (!double.TryParse(Console.ReadLine(), out double x)) return;

            Console.Write("Введите координату Y: ");
            if (!double.TryParse(Console.ReadLine(), out double y)) return;

            Point point = new Point(x, y);

            Func<double, double> f1 = (x) => -x * x + 4;
            Func<double, double> f2 = (x) => x * Math.Sin(x);
            Func<double, double> f3 = (x) => Math.Pow(x, 4) / 4 - Math.Pow(x, 3) / 3 - x * x;

            double[] eps = { 0.1, 0.01, 0.005, 0.001 };

            // Коллекция хранит логические значения для функций
            // 0: f1, 1: f2, 2: f3. Значения зависят от значений эпсилонов (eps).
            Dictionary<int, List<bool>> res = new Dictionary<int, List<bool>>()
            {
                { 0, new List<bool>() },
                { 1, new List<bool>() },
                { 2, new List<bool>() }
            };

            for (int i = 0; i < eps.Length; i++)
            {
                res[0].Add(point.IsNearFunc(f1, eps[i]));
                res[1].Add(point.IsNearFunc(f2, eps[i]));
                res[2].Add(point.IsNearFunc(f3, eps[i]));
            }

            string format = " f{0} | {1,-7} | {2,-8} | {3,-9} | {4,-9}";

            Console.WriteLine(format, " ", "e = 0,1", "e = 0,01", "e = 0,005", "e = 0,001");
            Console.WriteLine(new string('-', 49));

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(format, i+1, res[0][0], res[0][1], res[0][2], res[0][3]);
            }
        }
    }
}