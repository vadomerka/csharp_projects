﻿using System.Drawing;

namespace Program
{ 
    public static class Program
    {
        public static void Main(string[] args)
        {
            Rhombus.RhombusList rombs = null;
            string path = System.IO.Path.Combine("..", "..", "..", "..", "data", "data-9.txt");
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
                            rombs = new Rhombus.RhombusList(path);
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
                            Rhombus.RhombusSaver.SaveOG(rombs);
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
                            
                            Rhombus.RhombusSaver.SaveSorted(rombs);
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
                            if (rombs is not null)
                            {
                                var newRombs = rombs.Where(x => 5 < x.Space() && x.Space() < 40);
                                Rhombus.RhombusSaver.SaveSortedReverse(new Rhombus.RhombusList(newRombs.ToArray()));
                                Console.WriteLine("Данные сохранены.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Данные не считаны.");
                        }
                        break;
                    case ConsoleKey.D5:
                        double minVal = 15;
                        if (rombs is not null)
                        {
                            var newRombs = rombs.Where(x => x.Space() > minVal);
                            foreach (var rect in newRombs)
                            {
                                Console.WriteLine($"{rect}");
                            }
                        }
                        break;
                    case ConsoleKey.D6:
                        if (rombs is not null && rombs.OGRhombuses.Count() > 0)
                        {
                            double avge = rombs.Average(x => x.Space());
                            var newRombs = rombs.OGRhombuses.Where(x => x.Space() <= avge);
                            var enumer = newRombs.GetEnumerator();
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