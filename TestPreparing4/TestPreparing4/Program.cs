using System.Drawing;

namespace Program
{ 
    public static class Program
    {
        public static void Main(string[] args)
        {
            Rectangle.RectangleList rects = null;
            const string path = "../../../../data/data-2.txt";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите пункт меню.");
                Console.WriteLine("1) Считать данные из файла.");
                Console.WriteLine("2) Сохранить неупорядоченные данные.");
                Console.WriteLine("3) Сохранить упорядоченные данные в прямом порядке.");
                Console.WriteLine("4) Сохранить упорядоченные данные в обратном порядке.");
                Console.WriteLine("5) Запрос 1.");
                Console.WriteLine("6) Запрос 2.");
                ConsoleKey input = Console.ReadKey().Key;
                Console.WriteLine();
                switch (input)
                {
                    case ConsoleKey.D1:
                        try
                        {
                            rects = new Rectangle.RectangleList(path);
                            Console.WriteLine("Данные успешно считаны");
                        }
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine("Файл не найден. Добавьте файл в папку data.");
                            Console.WriteLine("И повторите попытку");
                        }
                        catch
                        {
                            Console.WriteLine("Произошла непредвиденная ошибка. Повторите попытку.");
                        }
                        break;
                    case ConsoleKey.D2:
                        try
                        {
                            Rectangle.RectangleSaver.SaveOG(rects);
                            Console.WriteLine("Данные сохранены.");
                        }
                        catch
                        {
                            Console.WriteLine("Данные не считаны.");
                        }
                        break;
                    case ConsoleKey.D3:
                        try
                        {
                            Rectangle.RectangleSaver.SaveSorted(rects);
                            Console.WriteLine("Данные сохранены.");
                        }
                        catch
                        {
                            Console.WriteLine("Данные не считаны.");
                        }
                        break;
                    case ConsoleKey.D4:
                        try
                        {
                            Rectangle.RectangleSaver.SaveSortedReverse(rects);
                            Console.WriteLine("Данные сохранены.");
                        }
                        catch
                        {
                            Console.WriteLine("Данные не считаны.");
                        }
                        break;
                    case ConsoleKey.D5:
                        double minVal = 15;
                        if (rects is not null)
                        {
                            var newRects = rects.Where(x => x.Perimetre() > minVal);
                            foreach (var r in newRects)
                            {
                                Console.WriteLine(r);
                            }
                        }
                        break;
                    case ConsoleKey.D6:
                        if (rects is not null && rects.OGRectangles.Count() > 0)
                        {
                            double avge = rects.Average(x => x.Perimetre());
                            var newRects = rects.Where(x => x.Perimetre() <= avge);
                            var enumer = rects.GetBackEnumerator();
                            while (enumer.MoveNext())
                            { 
                                Console.WriteLine($"{enumer.Current}");
                            }
                        }
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}