using GameClasses;
using System.IO;
using System.Text;

namespace Tetris
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            /*bool program = true;
            while (program)
            {
                Game game = new Game();
            }*/
            int n = 0;
            int d = 1;
            try
            {
                Console.WriteLine("Введите длину множества и глубину");
                n = int.Parse(Console.ReadLine() ?? "0");
                d = int.Parse(Console.ReadLine() ?? "1");
            } catch { }
            List<int> sets = new List<int>();
            for (int i = 0; i < n; i++)
            {
                sets.Add(n - i - 1);
            }
            List<List<int>> underSets = new List<List<int>>();
            for (long i = (long)Math.Pow(2, n); i < Math.Pow(2, n + 1); i++)
            {
                List<int> newUnderSet = new List<int>();
                string commands = Convert.ToString(i, d + 1)[1..];
                for (int nc = 0; nc < n; nc++)
                { 
                    char command = commands[nc];
                    if (command == '1') newUnderSet.Add(sets[nc]);
                }
                underSets.Add(newUnderSet);
            }
            Console.WriteLine($"Количество подмножеств глубины {}");
            /*
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var set in underSets)
            {
                foreach (var num in set)
                {
                    stringBuilder.Append($"{num} ");
                }
                stringBuilder.AppendLine();
            }
            Console.WriteLine(stringBuilder.ToString());*/
        }
    }
}