using FunctionLibrary;

namespace Main
{
    class Program
    {
        static void Main()
        {
            Function f = new Function((x) => Math.Sin(x));
            Function g = new Function((x) => x * x + x);
            CompFunction compF = new CompFunction(f, g);

            // Альтернативное решение для 10.
            //FunctionAlternative f = new(new FFunc());
            //FunctionAlternative g = new(new GFunc());
            //CompFunctionAlternative compF = new(f, g);

            Console.Write("Введите a: ");
            if (!double.TryParse(Console.ReadLine(), out double a)) return;

            Console.Write("Введите b: ");
            if (!double.TryParse(Console.ReadLine(), out double b)) return;

            Console.Write("Введите delta: ");
            if (!double.TryParse(Console.ReadLine(), out double d)) return;

            List<(double x, double value)> lst = new List<(double, double)>();

            for (double x = a; x <= b; x += d)
            {
                lst.Add((x, compF.Calculate(x)));
            }

            if (lst.Count == 0) return;

            Console.WriteLine(" {0,-5} | {1}", "x", "Значение");
            Console.WriteLine(new string('-', 18));

            foreach (var item in lst)
            {
                Console.WriteLine(" {0:f3} | {1:f3}", item.x, item.value);
            }
        }
    }
}